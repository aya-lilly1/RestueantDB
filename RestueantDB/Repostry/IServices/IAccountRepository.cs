using Microsoft.AspNetCore.Identity;
using RestueantDB.ModelViews;
using System.Threading.Tasks;

namespace RestueantDB.Repostry.IServices
{
    public interface IAccountRepository
    {
        Task<IdentityResult> CreateUser(SignUpUser userModel);
        Task<SignInResult> LogInUser(LoginUser user);
        Task LogOut();
        Task<IdentityResult> ChangePasswordAsync(ChangePassword password);
        Task<IdentityResult> ConfirmEmal(string uid, string token);
        Task GenerateEmailConfirmationTokenAsync(IdentityUser user);
        Task<IdentityUser> GetUserByEmailAsync(string email);
        Task GenerateForgotPasswordTokenAsync(IdentityUser user);
        Task<IdentityResult> ResentPassword(ResetPassword model);
    }
}
