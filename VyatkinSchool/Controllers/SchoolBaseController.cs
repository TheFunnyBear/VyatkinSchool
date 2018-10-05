using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using VyatkinSchool.Models;
using VyatkinSchool.Models.IdentityModels;

namespace VyatkinSchool.Controllers
{
    public abstract class SchoolBaseController : Controller
    {
        public async Task IncrementPageCounters()
        {
            var absolutePath = HttpContext.Request.Url.AbsolutePath;
            ViewBag.VisitsCount = await IncrementPageCountersTask(absolutePath);

            var remoteIpAddress = HttpContext.Request.UserHostAddress;
            await AddUserHostAddressTask(remoteIpAddress);
        }

        async private Task<int> IncrementPageCountersTask(string absolutePath)
        {
            int result = 1;
            return await Task.Run(() =>
            {
                using (var dataBase = new ApplicationDbContext())
                {
                    var counter = dataBase.VisitsCounters.SingleOrDefault(visitsCounter => visitsCounter.AbsolutePath.Equals(absolutePath));
                    if (counter == null)
                    {
                        var counterItem = new VisitsCounterItem();
                        counterItem.AbsolutePath = absolutePath;
                        counterItem.VisitsCount = 1;
                        dataBase.VisitsCounters.Add(counterItem);
                        dataBase.SaveChanges();
                        result = counterItem.VisitsCount;
                    }
                    else
                    {
                        counter.VisitsCount++;
                        dataBase.SaveChanges();
                        result = counter.VisitsCount;
                    }
                }
                return result;
            });
        }

        async private Task AddUserHostAddressTask(string hostAddress)
        {
            await Task.Run(() =>
            {
                using (var dataBase = new ApplicationDbContext())
                {
                    var host = dataBase.UserHostsAddress.SingleOrDefault(userHostsAddress => userHostsAddress.HostAddress.Equals(hostAddress));
                    if (host == null)
                    {
                        var userHostsAddress = new UserHostAddress();
                        userHostsAddress.HostAddress = hostAddress;
                        userHostsAddress.LastVisit = DateTime.Now;
                        dataBase.UserHostsAddress.Add(userHostsAddress);
                        dataBase.SaveChanges();
                    }
                    else
                    {
                        host.LastVisit = DateTime.Now;
                        dataBase.SaveChanges();
                    }
                }
            });
        }
    }
}