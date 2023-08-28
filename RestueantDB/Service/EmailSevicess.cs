using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using RestueantDB.ModelViews;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace RestueantDB.Service
{
    public class EmailSevicess : IEmailSevicess
    {
        private const string templatePath = "/EmailTemplate/{0}.html";
        private readonly IOptions<SMTPConfig> _smtpconfig;
        private readonly IWebHostEnvironment _host;
        public EmailSevicess(IOptions<SMTPConfig> smtpconfig, IWebHostEnvironment host)
        {
            _smtpconfig = smtpconfig;
            _host = host;
        }

        public async Task SendTestEmail(UserEmailOptions userEmailOptions)
        {

            userEmailOptions.Subject = UpdatePlaceHolders(userEmailOptions.Subject,userEmailOptions.PlaceHolders);
            userEmailOptions.Body = UpdatePlaceHolders(GetEmailBody("htmlpage"), userEmailOptions.PlaceHolders);
            await SendEmail(userEmailOptions);

        }

        public async Task SendEmailconfirmation(UserEmailOptions userEmailOptions)
        {

            userEmailOptions.Subject =    UpdatePlaceHolders("Hello {{UserName}} Confirmation Your Email", userEmailOptions.PlaceHolders);
            userEmailOptions.Body = UpdatePlaceHolders(GetEmailBody("EmailConfirmation"), userEmailOptions.PlaceHolders);
            await SendEmail(userEmailOptions);

        }
        public async Task SendEmailForForgotPassword(UserEmailOptions userEmailOptions)
        {

            userEmailOptions.Subject = UpdatePlaceHolders("Hello {{UserName}} rest your Password", userEmailOptions.PlaceHolders);
            userEmailOptions.Body = UpdatePlaceHolders(GetEmailBody("ForgotPassword"), userEmailOptions.PlaceHolders);
            await SendEmail(userEmailOptions);

        }



        private async Task SendEmail(UserEmailOptions userEmailOptions)
        {
            MailMessage Mail = new MailMessage
            {

                Subject = userEmailOptions.Subject,

                Body = userEmailOptions.Body,
                From = new MailAddress(_smtpconfig.Value.SenderAddress, _smtpconfig.Value.SenderDisaplayName),
                IsBodyHtml = _smtpconfig.Value.IsBodyHTML

            };

            foreach (var toEmail in userEmailOptions.ToEmail)
            {
                Mail.To.Add(toEmail);
            }


            NetworkCredential networkCredential = new NetworkCredential(_smtpconfig.Value.Username, _smtpconfig.Value.Password);
            SmtpClient smtpClient = new SmtpClient
            {
                Host = _smtpconfig.Value.Host,
                Port = _smtpconfig.Value.Port,
                EnableSsl = _smtpconfig.Value.EnableSSl,
                UseDefaultCredentials = _smtpconfig.Value.UseDefaultCredentials,
                Credentials = networkCredential
            };


            Mail.BodyEncoding = Encoding.Default;
            await smtpClient.SendMailAsync(Mail);
        }


        
         //we can get body from folder or we put the code html here. 

        private string GetEmailBody(string templateName)
         {
            var fileHTML = string.Format(templatePath, templateName);
            var fullPath = _host.ContentRootPath + fileHTML;

                var body = File.ReadAllText(fullPath);

//            var body = $@"<!DOCTYPE html>
//<html>
//<head>
//    <meta charset=""utf-8""/>
//    <title></title>
//<head>
//<body>
//    <p> Hello From Aya World </p>
//</body>
//</html>";

            return body;
        }

        private string UpdatePlaceHolders(string text, List<KeyValuePair<string,string>> keyValuePairs)
        {
            if(!string.IsNullOrEmpty(text) && keyValuePairs != null)
            {
                foreach(var placeHolder in keyValuePairs)
                {
                    if (text.Contains(placeHolder.Key))
                    {
                        text = text.Replace(placeHolder.Key, placeHolder.Value);
                    }
                }

            }
            return text;
        }
    }
}
