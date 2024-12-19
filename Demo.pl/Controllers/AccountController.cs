using Demo.DAL.Models;
using Demo.pl.Helpers;
using Demo.pl.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Demo.pl.Controllers
{
   
    public class AccountController : Controller
    {
		private readonly UserManager<ApplactionUser> _userManager;
		private readonly SignInManager<ApplactionUser> _signInManager;

		public AccountController( UserManager<ApplactionUser> userManager,SignInManager<ApplactionUser> signInManager)
        {
			_userManager = userManager;
			_signInManager = signInManager;
		}
        #region SingUp
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {

                var user = new ApplactionUser
                {
                    UserName = model.Email.Split("@")[0],
                    IsAgree = model.IsAgree,
                    Email = model.Email,
                    LName = model.LName,
                    FName = model.FName,


                };

				  var result= await _userManager.CreateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(SignIn));
                }
                foreach ( var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
			}
			return View(model);
		}
        #endregion

        #region SignIn

        public IActionResult SignIn()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (ModelState.IsValid)
            {
              var user= await _userManager.FindByEmailAsync(model.Email);

                if(user is not null)
                {
                    bool flag= await _userManager.CheckPasswordAsync(user, model.Password);

                    if (flag)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                        if (result.Succeeded)
                        {
                            return RedirectToAction(nameof(HomeController.Index),"Home");
                        }
                    }
                }

                ModelState.AddModelError(string.Empty, "Invalid Login");

            }

            return View(model);
        }


        #endregion

        #region Sign Out
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(SignIn));
        }


		#endregion

		#region Forget Password

        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var ResetPassword = Url.Action("ResetPassword", "Account", new { email = model.Email ,token = token});
                    var email = new Email()
                    {
                        Subject = "Reset Your Password",
                        Recipents = model.Email,
                        Body = ResetPassword
                    };
                    EmailSettings.sendEmail(email);
                    return RedirectToAction(nameof(CheckYourInbox));

                }
                ModelState.AddModelError(string.Empty, "Invalid Email");
            }

            return View(model);
        }

        public IActionResult CheckYourInbox()
        {
            return View();
        }




        public IActionResult ResetPassword( string email, string token)
        {

            TempData["email"]=email;
            TempData["token"]=token;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model) 
        {
            if (ModelState.IsValid)
            {
                string email = TempData["email"] as string;
                string token = TempData["token"] as string;

                var user=await  _userManager.FindByEmailAsync(email);
                var result = await _userManager.ResetPasswordAsync(user, token, model.newPassword);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(SignIn));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
           
        }


        #endregion



    }
}
