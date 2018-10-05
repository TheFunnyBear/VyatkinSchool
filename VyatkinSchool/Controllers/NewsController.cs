using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VyatkinSchool.Models;

namespace VyatkinSchool.Controllers
{
    [Authorize]
    [RequireHttps]
    public class NewsController : SchoolBaseController
    {
        // GET: News
        [AllowAnonymous]
        public ActionResult Index()
        {
            return RedirectToAction("SixNews", "News");
        }

        [AllowAnonymous]
        public async Task<ActionResult> SixNews(int? page)
        {
            await IncrementPageCounters();
            var viewModel = new SixNewsViewModel();
            viewModel.PageNumber = page ?? 1;
            return View(viewModel);
        }
    }
}