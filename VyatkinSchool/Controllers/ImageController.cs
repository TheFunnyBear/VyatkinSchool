using System.Web.Mvc;
using VyatkinSchool.Models;
using System.Linq;
using System.IO;
using System.Drawing;

namespace VyatkinSchool.Controllers
{
    public class ImageController : Controller
    {
        public ActionResult Show(int id)
        {
            using (var dataBase = new GalleryManagerEntities())
            {
                var galleryItem = dataBase.Gallery.SingleOrDefault(item => item.ID == id);
                var fileName = $"{Path.GetFileNameWithoutExtension(Path.GetRandomFileName())}.jpg";
                return File(galleryItem.Image, "image/jpg", fileName);
            }
        }
    }
}