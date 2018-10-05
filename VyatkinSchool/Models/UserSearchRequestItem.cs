using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace VyatkinSchool.Models
{
    public sealed class UserSearchRequestItem
    {
        public UserSearchRequestItem()
        {
            SearchInNews = true;
            SearchInPhoto = true;
            SearchInVideo = true;
        }

        public int Id { get; set; }
        [Display(Name = "Искть текст:")]
        [Required(ErrorMessage = "Пожалуйста, введите заголовок для изображения")]
        public string SearchRequest { get; set; }
        [Display(Name = "Искть в новостях:")]
        public bool SearchInNews { get; set; }
        [Display(Name = "Искть в фотографиях:")]
        public bool SearchInPhoto { get; set; }
        [Display(Name = "Искть в видео:")]
        public bool SearchInVideo { get; set; }

        internal bool Include(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            if (!text.Contains(SearchRequest))
            {
                var wordsForSharch = SearchRequest.Split(new[] { ' ', ',', '.', '\\', '-' });
                return wordsForSharch.Any(word => text.Contains(word));
            }
            else
            {
                return true;
            }
            
        }
    }
}