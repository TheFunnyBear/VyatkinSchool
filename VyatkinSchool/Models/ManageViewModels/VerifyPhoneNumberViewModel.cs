using System.ComponentModel.DataAnnotations;

namespace VyatkinSchool.Models.ManageViewModels
{
    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = "Код")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }
    }
}