using System.Web.Mvc;
using VyatkinSchool.Models;

namespace VyatkinSchool.Controllers
{
    [Authorize]
    [RequireHttps]
    public class NewsController : Controller
    {
        // GET: News
        [AllowAnonymous]
        public ActionResult Index()
        {
            return RedirectToAction("SixNews", "News");
        }

        [AllowAnonymous]
        public ActionResult SixNews(int? page)
        {
            var viewModel = new SixNewsViewModel();
            viewModel.PageNumber = page ?? 1;
            return View(viewModel);
        }
    }
}