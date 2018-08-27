using System.ComponentModel.DataAnnotations;

namespace VyatkinSchool.Models.AcountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Почта")]
        public string Email { get; set; }
    }
}
