using System.ComponentModel.DataAnnotations;

namespace Demo.pl.ViewModels
{
    public class ResetPasswordViewModel
    {

        [Required(ErrorMessage="Password Is Required")]
        [DataType(DataType.Password)]
        public string newPassword { get; set; }
        [Required(ErrorMessage = "Confirm Password Is Required")]
        [DataType (DataType.Password)]
        [Compare(nameof(newPassword),ErrorMessage = "Confirm Password does't Match Newpassword")]
        public string ConfirmPassword { get; set; }
    }
}
