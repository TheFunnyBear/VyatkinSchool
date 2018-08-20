using System.ComponentModel.DataAnnotations;

namespace VyatkinSchool.Models
{

    public class GalleryGroup
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите заголовок для альбома изображений")]
        public string GroupName { get; set; }
    }
}