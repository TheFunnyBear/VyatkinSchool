using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using VyatkinSchool.Models.IdentityModels;

namespace VyatkinSchool.Controllers
{
    [Authorize]
    [RequireHttps]
    public class ImageController : SchoolBaseController
    {
        [AllowAnonymous]
        public async Task<ActionResult> Show(int id)
        {
            await IncrementPageCounters();
            using (var dataBase = new ApplicationDbContext())
            {
                var galleryItem = dataBase.Images.SingleOrDefault(item => item.Id == id);
                var fileName = $"{Path.GetFileNameWithoutExtension(Path.GetRandomFileName())}.jpg";
                return File(galleryItem.Image, "image/jpg", fileName);
            }
        }

        [AllowAnonymous]
        public async Task<ActionResult> ShowImageForGalleryItem(int id)
        {
            await IncrementPageCounters();
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