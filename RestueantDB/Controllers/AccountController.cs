
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RestueantDB.ModelViews;
using RestueantDB.Repostry.IServices;
using System.Threading.Tasks;

namespace RestueantDB.Controllers
{
    public class AccountController : Controller
    {
        private IAccountRepository _account;
        private UserManager<IdentityUser> _userManager;
        public AccountController(IAccountRepository account , UserManager<IdentityUser> userManager)
        {
            _account = account;
            _userManager = userManager;
        }

        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpUser user)
        {
            if (ModelState.IsValid)
            {
                var result = await _account.CreateUser(user);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(user);
                }

                    ModelState.Clear();
                    return RedirectToAction("ConfirmEmail", new { Email = user.Email });

                
            }
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUser user, string returnUrl)
        {
            if (ModelState.IsValid)
            {

                var result = await _account.LogInUser(user);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        if(returnUrl.Contains("forgot", System.StringComparison.InvariantCultureIgnoreCase ) 
                            || returnUrl.Contains("reset", System.StringComparison.InvariantCultureIgnoreCase))
                        {

                            return RedirectToAction("Index", "Home");
                        }
                        return LocalRedirect(returnUrl);
                    }

                    return RedirectToAction("Index", "Home");
                }
                if (result.IsNotAllowed)
                {
                    ModelState.AddModelError("", "Invalid Allowed");
                }
                else 
                { 
                    ModelState.AddModelError("", "Invalid");
                }
               
            }
            return View(user);
        }

        public async Task<IActionResult> LogOut()
        {
            await _account.LogOut();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> ChangePasswordAsync(ChangePassword password)
        {
            if (ModelState.IsValid)
            {

                var result = await _account.ChangePasswordAsync(password);

                if (result.Succeeded)
                {
                    ViewBag.IsCompleted = true;
                    return View(password);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", "Invalid");
                    }

                }

            }
            return View(password);
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string uid, string token,string email)
        {
            EmailConfirmMV model = new EmailConfirmMV
            {
                Email = email
            };


            if (!string.IsNullOrEmpty(uid) && token != null)
            {
                token = token.Replace(" ", "+");
               var result = await _account.ConfirmEmal(uid, token);
                if (result.Succeeded)
                {
                    model.EmailVerified = true;
                }

            }
            return View(model);
        }

        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(EmailConfirmMV model)
        {
            var user = await _account.GetUserByEmailAsync(model.Email);
            if (user != null)
            {
                if (user.EmailConfirmed)
                {
                    model.IsConfirmed = true;

                    return View(model);
                }
                await _account.GenerateEmailConfirmationTokenAsync(user);
                model.EmailSent = true;
                //model.EmailVerified= true;
                ModelState.Clear();
            }
            else
            {
                ModelState.AddModelError("", "Somthing Error");
            }

            
            return View(model);
        }

        [AllowAnonymous,HttpGet("forgot-password")]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [AllowAnonymous, HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPassword model)
        {
            if (ModelState.IsValid)
            {
                var user =await _account.GetUserByEmailAsync(model.Email);
                if(user != null)
                {
                    await _account.GenerateForgotPasswordTokenAsync(user);
                    ModelState.Clear();
                    model.EmailSent = true;
                }
               
            }
            return View(model);
        }


        [ HttpGet("reset-password")]
        public IActionResult ResentPassword( string uId ,string token)
        {
            ResetPassword resetPassword = new ResetPassword
            {
                Token=token,
                UserId=uId
            };

            return View(resetPassword);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResentPassword(ResetPassword model)
        {
            if (ModelState.IsValid)
            {
                model.Token = model.Token.Replace(" ", "+");
                var result = await _account.ResentPassword(model);
                if (result.Succeeded)
                {
                    ModelState.Clear();
                    model.IsSuccess = true;
                    return View(model);
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

              
            }
            return View();
        }

        [AcceptVerbs("Post","Get")]
        [AllowAnonymous]
        public async Task<IActionResult> ValiateEmail(string email)
        {
            var result = await _userManager.FindByEmailAsync(email);
            if(result == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Emial {email} is already exist");
            }

        }
    }
}
