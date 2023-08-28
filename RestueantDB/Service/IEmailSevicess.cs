using RestueantDB.ModelViews;
using System.Threading.Tasks;

namespace RestueantDB.Service
{
    public interface IEmailSevicess
    {
        Task SendTestEmail(UserEmailOptions userEmailOptions);
        Task SendEmailconfirmation(UserEmailOptions userEmailOptions);
        Task SendEmailForForgotPassword(UserEmailOptions userEmailOptions);
    }
}