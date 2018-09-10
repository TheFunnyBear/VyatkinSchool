using System.Web.Mvc;
using VyatkinSchool.Models;

namespace VyatkinSchool.Controllers
{
    [Authorize]
    [RequireHttps]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return RedirectToAction("SixNews", "News");
        }

        [AllowAnonymous]
        public ActionResult Contacts()
        {
            var contactsViewModel = new Contacts();
            return View(contactsViewModel);
        }

        [AllowAnonymous]
        public ActionResult TimeTable()
        {
            return View();
        }
    }
}