using System.Web;
using System.Web.Mvc;

namespace VyatkinSchool.Controllers
{
    public class ErrorController : SchoolBaseController
    {
        //
        // GET: /Error/

        public ActionResult Index()
        {
            IncrementPageCounters();
            return RedirectToAction("GenericError", new HandleErrorInfo(new HttpException(403, "Don't allow access the error pages"), "ErrorController", "Index"));
        }

        public ViewResult GenericError(HandleErrorInfo exception)
        {
            IncrementPageCounters();
            return View("Error", exception);
        }

        public ViewResult NotFound(HandleErrorInfo exception)
        {
            IncrementPageCounters();
            ViewBag.Title = "Page Not Found";
            return View("Error", exception);
        }
    }
}