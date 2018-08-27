using System.Web.Mvc;
using VyatkinSchool.Models;

namespace VyatkinSchool.Controllers
{
    public class NewsController : Controller
    {
        // GET: News
        public ActionResult Index()
        {
            return RedirectToAction("SixNews", "News");
        }

        public ActionResult SixNews(int? page)
        {
            var viewModel = new SixNewsViewModel();
            viewModel.PageNumber = page ?? 1;
            return View(viewModel);
        }
    }
}