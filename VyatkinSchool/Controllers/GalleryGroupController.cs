using System;
using System.Linq;
using System.Web.Mvc;
using VyatkinSchool.Models;
using VyatkinSchool.Models.IdentityModels;

namespace VyatkinSchool.Controllers
{
    [Authorize]
    public class GalleryGroupController : Controller
    {
        // GET: GalleryGroup
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Message/Add
        public ActionResult Add()
        {
            var galleryGroupModel = new GalleryGroupItem();
            return View(galleryGroupModel);
        }

        // POST: /GalleryGroup/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(GalleryGroupItem viewModel)
        {
            if (ModelState.IsValid)
            {
                using (var dataBase = new ApplicationDbContext())
                {
                    var existedGalleryGroups = dataBase.GalleryGroups.ToList();
                    var alreadyExistedGroup = existedGalleryGroups.SingleOrDefault(group => group.GroupName.Equals(viewModel.GroupName, StringComparison.OrdinalIgnoreCase));
                    if (alreadyExistedGroup == null)
                    {
                        dataBase.GalleryGroups.Add(viewModel);
                        dataBase.SaveChanges();
                    }

                    existedGalleryGroups = dataBase.GalleryGroups.ToList();
                    ViewBag.ExistedGalleryGroups = existedGalleryGroups;

                    if (alreadyExistedGroup != null)
                    {
                        //Already exist
                        return View("AlreadyExist", alreadyExistedGroup);
                    }
                }
                ModelState.Clear();
            }

            //Successfully created 
            return View("Created", viewModel);
        }

        // GET: /Message/Created
        public ActionResult Created()
        {
            return View();
        }

        // GET: /Message/AlreadyExist
        public ActionResult AlreadyExist()
        {
            return View();
        }

        // GET: /Message/List
        public ActionResult List()
        {
            using (var dataBase = new ApplicationDbContext())
            {
                ViewBag.ExistedGalleryGroups = dataBase.GalleryGroups.ToList();
                return View();
            }
        }
    }
}