using System.Linq;
using System.Web.Mvc;
using VyatkinSchool.Models;
using VyatkinSchool.Models.IdentityModels;

namespace VyatkinSchool.Controllers
{
    public abstract class SchoolBaseController : Controller
    {
        public void IncrementPageCounters()
        {
            var absolutePath = HttpContext.Request.Url.AbsolutePath;

            using (var dataBase = new ApplicationDbContext())
            {
                var counter = dataBase.VisitsCounters.SingleOrDefault(visitsCounter => visitsCounter.AbsolutePath.Equals(absolutePath));
                if (counter == null)
                {
                    var counterItem = new VisitsCounterItem();
                    counterItem.AbsolutePath = absolutePath;
                    counterItem.VisitsCount = 1;
                    ViewBag.VisitsCount = counterItem.VisitsCount;
                    dataBase.VisitsCounters.Add(counterItem);
                    dataBase.SaveChanges();
                }
                else
                {
                    counter.VisitsCount++;
                    ViewBag.VisitsCount = counter.VisitsCount;
                    dataBase.SaveChanges();
                }
            }
        }
    }
}