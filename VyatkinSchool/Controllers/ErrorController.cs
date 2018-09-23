using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace VyatkinSchool.Controllers
{
    public class ErrorController : SchoolBaseController
    {
        //
        // GET: /Error/

        public async Task<ActionResult> Index()
        {
            await IncrementPageCounters();
            return RedirectToAction("GenericError", new HandleErrorInfo(new HttpException(403, "Don't allow access the error pages"), "ErrorController", "Index"));
        }

        public async Task<ViewResult> GenericError(HandleErrorInfo exception)
        {
            await IncrementPageCounters();
            return View("Error", exception);
        }

        public async Task<ViewResult> NotFound(HandleErrorInfo exception)
        {
            await IncrementPageCounters();
            ViewBag.Title = "Page Not Found";
            return View("Error", exception);
        }
    }
}