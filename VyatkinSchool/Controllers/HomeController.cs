using System.Web.Mvc;
using VyatkinSchool.Models;

namespace VyatkinSchool.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("SixNews", "News");
        }

        public ActionResult Contacts()
        {
            var contactsViewModel = new Contacts();
            return View(contactsViewModel);
        }

        public ActionResult TimeTable()
        {
            return View();
        }
    }
}