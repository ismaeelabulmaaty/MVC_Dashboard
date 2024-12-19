﻿

using System.ComponentModel.DataAnnotations;

namespace Demo.pl.ViewModels
{
    public class ForgetPasswordViewModel
    {

        [Required(ErrorMessage ="Email Is Required")]
        [EmailAddress(ErrorMessage ="Invalid Email")]
        public string Email { get; set; }
    }
}
