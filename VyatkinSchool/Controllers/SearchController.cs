using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using VyatkinSchool.Models;
using VyatkinSchool.Models.IdentityModels;

namespace VyatkinSchool.Controllers
{
    [Authorize]
    [RequireHttps]
    public class SearchController : SchoolBaseController
    {
        // GET: News
        [AllowAnonymous]
        public ActionResult Index()
        {
            var request = new UserSearchRequestItem();
            return View(request);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(UserSearchRequestItem viewModel)
        {
            var shearchResults = new List<UserSearchResultItem>();
            if (ModelState.IsValid)
            {
                using (var dataBase = new ApplicationDbContext())
                {
                    dataBase.UserSearchRequests.Add(viewModel);
                    dataBase.SaveChanges();

                    if (viewModel.SearchInNews)
                    {
                        foreach (var message in dataBase.Messages.Where(item => item.IsDeleted == false))
                        {
                            if (viewModel.Include(message.Title) || viewModel.Include(message.MessageText))
                            {
                                shearchResults.Add(new UserSearchResultItem()
                                {
                                    ContntType = ContentType.News,
                                    ContentId = message.Id,
                                    RequestId = viewModel.Id,
                                    SearchTitle = message.Title,
                                    SearchText = message.MessageText
                                });
                            }
                        }
                    }

                    if (viewModel.SearchInPhoto)
                    {
                        foreach (var galleryItem in dataBase.Gallery.Where(item => item.IsDeleted == false))
                        {
                            if (viewModel.Include(galleryItem.Title) || viewModel.Include(galleryItem.Description))
                            {
                                shearchResults.Add(new UserSearchResultItem()
                                {
                                    ContntType = ContentType.Gallery,
                                    ContentId = galleryItem.Id,
                                    RequestId = viewModel.Id,
                                    SearchTitle = galleryItem.Title,
                                    SearchText = galleryItem.Description
                                });
                            }
                        }
                    }

                    if (viewModel.SearchInVideo)
                    {
                        foreach (var videoItem in dataBase.Video.Where(item => item.IsDeleted == false))
                        {
                            if (viewModel.Include(videoItem.Title) || viewModel.Include(videoItem.Description))
                            {
                                shearchResults.Add(new UserSearchResultItem()
                                {
                                    ContntType = ContentType.Video,
                                    ContentId = videoItem.Id,
                                    RequestId = viewModel.Id,
                                    SearchTitle = videoItem.Title,
                                    SearchText = videoItem.Description
                                });
                            }
                        }
                    }
                }

                ViewBag.SearchRequest = viewModel.SearchRequest;

                //Successfully created
                return View("SearchResults", shearchResults);
            }

            ModelState.Clear();
            return RedirectToAction("Index", "Search");

        }

        [AllowAnonymous]
        public ActionResult SearchResults(int? page)
        {
            return View();
        }
    }
}