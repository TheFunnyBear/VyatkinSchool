using System.ComponentModel.DataAnnotations;

namespace VyatkinSchool.Models
{
    public sealed class GalleryGroupItem
    {
        public int Id { get; set; }

        [Display(Name = "Укажите название нового альбома:")]
        [Required(ErrorMessage = "Пожалуйста, введите заголовок для альбома изображений")]
        public string GroupName { get; set; }
    }
}