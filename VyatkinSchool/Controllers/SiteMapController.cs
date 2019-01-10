using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using VyatkinSchool.Helpers;
using VyatkinSchool.Models.IdentityModels;

namespace VyatkinSchool.Controllers
{
    public class SiteMapController : Controller
    {
        // GET: SiteMap
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetSiteMap()
        {
            using (var dataBase = new ApplicationDbContext())
            {
                var notDeletedMessages = dataBase.Messages
                    .Where(message => !message.IsDeleted)
                    .OrderByDescending(message => message.Date)
                    .ToList();

                var sitemapGenerator = new SitemapGenerator(notDeletedMessages);
                var sitemapNodes = sitemapGenerator.GetSitemapNodes(this.Url);
                string xml = sitemapGenerator.GetSitemapDocument(sitemapNodes);
                return this.Content(xml, "text/xml", Encoding.UTF8);
            }
        }
    }
}