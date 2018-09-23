using System.Threading.Tasks;
using System.Web.Mvc;
using VyatkinSchool.Models;

namespace VyatkinSchool.Controllers
{
    [Authorize]
    [RequireHttps]
    public class HomeController : SchoolBaseController
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return RedirectToAction("SixNews", "News");
        }

        [AllowAnonymous]
        public async Task<ActionResult> Contacts()
        {
            await IncrementPageCounters();
            var contactsViewModel = new Contacts();
            return View(contactsViewModel);
        }

        [AllowAnonymous]
        public async Task<ActionResult> TimeTable()
        {
            await IncrementPageCounters();
            return View();
        }
    }
}