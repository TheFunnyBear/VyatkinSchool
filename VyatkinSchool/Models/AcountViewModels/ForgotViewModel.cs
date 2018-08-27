using System.ComponentModel.DataAnnotations;

namespace VyatkinSchool.Models.AcountViewModels
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Адрес электронной почты")]
        public string Email { get; set; }
    }
}
