using System.ComponentModel.DataAnnotations;

namespace Demo.pl.ViewModels
{
	public class SignUpViewModel
	{
		[Required(ErrorMessage = "FName Is Required")]
		public string FName { get; set; }

		[Required(ErrorMessage = "LName Is Required")]
		public string LName { get; set; }

		[Required(ErrorMessage ="Email Is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string  Email { get; set; }
		[Required(ErrorMessage = "Password Is Required")]
        [DataType(DataType.Password)]
		public string Password { get; set; }
		[Required(ErrorMessage = " Confirm Password Is Required")]
		[Compare(nameof(Password),ErrorMessage= "Confirm Password Dosnt Match Password")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }
		[Required(ErrorMessage = "IsAgree Is Required")]
		public bool IsAgree { get; set; }
    }
}
