using System.IO;
using System.Linq;
using System.Web.Mvc;
using VyatkinSchool.Models.IdentityModels;

namespace VyatkinSchool.Controllers
{
    public class ImageController : Controller
    {
        public ActionResult Show(int id)
        {
            using (var dataBase = new ApplicationDbContext())
            {
                var galleryItem = dataBase.Images.SingleOrDefault(item => item.Id == id);
                var fileName = $"{Path.GetFileNameWithoutExtension(Path.GetRandomFileName())}.jpg";
                return File(galleryItem.Image, "image/jpg", fileName);
            }
        }

        public ActionResult ShowImageForGalleryItem(int id)
        {
            using (var dataBase = new ApplicationDbContext())
            {
                var gallery = dataBase.Gallery.SingleOrDefault(galleryItem => galleryItem.Id == id);
                var imageItem = dataBase.Images.SingleOrDefault(item => item.Id == gallery.ImageId);
                var fileName = $"{Path.GetFileNameWithoutExtension(Path.GetRandomFileName())}.jpg";
                return File(imageItem.Image, "image/jpg", fileName);
            }
        }
    }
}