using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using RestueantDB.ModelViews;
using RestueantDB.Repostry.IServices;
using RestueantDB.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestueantDB.Repostry.Services
{
    public class AccountRepository : IAccountRepository
    {
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;
        private IUserServices _userServices;
        private IEmailSevicess _emailSevicess;
        private IConfiguration _configration;
        public AccountRepository(UserManager<IdentityUser> userManager,
                                     SignInManager<IdentityUser> signInManager,
                                      IUserServices userServices,
                                      IEmailSevicess emailSevicess,
                                      IConfiguration configration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userServices = userServices;
            _emailSevicess = emailSevicess;
            _configration = configration;
        }
        public async Task<IdentityUser> GetUserByEmailAsync( string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user;

        }
        public async Task<IdentityResult> CreateUser(SignUpUser userModel)
        {
            var user = new IdentityUser()
            {
                UserName = userModel.UserName,
                Email = userModel.Email,

            };
            var result =await _userManager.CreateAsync(user, userModel.Password);
            if (result.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                if (!string.IsNullOrEmpty(token))
                {
                    await GenerateEmailConfirmationTokenAsync(user);
                }
            }
            return result;
        }

        public async Task GenerateEmailConfirmationTokenAsync(IdentityUser user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            if (!string.IsNullOrEmpty(token))
            {
                await SendEmailconfirmation(user, token);
            }
        }

        public async Task GenerateForgotPasswordTokenAsync(IdentityUser user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            if (!string.IsNullOrEmpty(token))
            {
                await SendForgotPassword(user, token);
            }
        }
        public async Task<SignInResult> LogInUser(LoginUser user)
        {
            if (user.Email.Contains("@"))
            {
                var User = await _userManager.FindByEmailAsync(user.Email);
                 user.Email = User?.UserName ?? user.Email;
            }
            var result = await _signInManager.PasswordSignInAsync(user.Email,user.Password, user.RememberMe,false);
            return result;

        }
        public async Task LogOut()
        {
           await _signInManager.SignOutAsync();
        }
       
        public async Task<IdentityResult> ChangePasswordAsync(ChangePassword password)
        {
             
            var userId = _userServices.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            return await _userManager.ChangePasswordAsync(user, password.CurrentPassword, password.NewPassword);
          
        }
        public async Task<IdentityResult> ConfirmEmal(string uid , string token)
        {
            var user = await _userManager.FindByIdAsync(uid);
            return await _userManager.ConfirmEmailAsync(user,token);

        }
        public async Task<IdentityResult> ResentPassword( ResetPassword model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            return await _userManager.ResetPasswordAsync(user,model.Token,model.NewPassword);

        }

        private async Task SendEmailconfirmation(IdentityUser user , string token)
        {
            string appDomain = _configration.GetSection("Application:AppDomain").Value;
            string confirmationLink = _configration.GetSection("Application:Emailconfirmation").Value;

            UserEmailOptions options = new UserEmailOptions
            {
                ToEmail = new List<string>() { user.Email },
                PlaceHolders = new List<KeyValuePair<string, string>>(){
                    new KeyValuePair<string, string>("{{UserName}}",user.UserName),
                     new KeyValuePair<string, string>("{{Link}}",string.Format(appDomain+confirmationLink,user.Id,token))
                      
                },
            };
            await _emailSevicess.SendEmailconfirmation(options);
        }

        private async Task SendForgotPassword(IdentityUser user, string token)
        {
            string appDomain = _configration.GetSection("Application:AppDomain").Value;
            string confirmationLink = _configration.GetSection("Application:ForgotPassword").Value;

            UserEmailOptions options = new UserEmailOptions
            {
                ToEmail = new List<string>() { user.Email },
                PlaceHolders = new List<KeyValuePair<string, string>>(){
                    new KeyValuePair<string, string>("{{UserName}}",user.UserName),
                     new KeyValuePair<string, string>("{{Link}}",string.Format(appDomain+confirmationLink,user.Id,token))

                },
            };
            await _emailSevicess.SendEmailForForgotPassword(options);
        }
    }
}
