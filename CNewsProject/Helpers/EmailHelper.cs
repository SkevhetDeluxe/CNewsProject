using MimeKit;
using MailKit.Net.Smtp;
using MimeKit.Text;
using Microsoft.AspNetCore.Identity.UI.Services;
using CNewsProject.Models.DataBase.Identity;
//using System.Net.Mail;

namespace CNewsProject.Helpers
{
    public class EmailHelper //: IEmailSender
    {
        private IConfiguration _configuration;

        public EmailHelper(IConfiguration config)
        {
            _configuration = config;
        }

        #region YogiGhosting NFED
        //public bool SendEmail(string userEmail, string confirmationLink)
        //{
        //    MailMessage mailMessage = new MailMessage();
        //    mailMessage.From = new MailAddress("info.cnewsproject@gmail.com");
        //    mailMessage.To.Add(new MailAddress(userEmail));

        //    mailMessage.Subject = "Confirm your email";
        //    mailMessage.IsBodyHtml = true;
        //    mailMessage.Body = confirmationLink;

        //    System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
        //    client.Credentials = new System.Net.NetworkCredential("info.cnewsproject@gmail.com", "SugMinRollBitch1337420"); // SugMinRollBitch1337420
        //    client.Host = "smtpout.secureserver.net";
        //    client.Port = 80;

        //    try
        //    {
        //        client.Send(mailMessage);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        // log exception
        //    }
        //    return false;
        //}
        #endregion

        //ROBERTS WAY. VI FÅR SE
        #region ROBERTS WAY. VI FÅR SE
        public bool SendEmailAsync(string email, string subject, string content)
        {
            var message = new MimeMessage();
            message.Sender = MailboxAddress.Parse(_configuration["SenderEmail"]);
            message.Sender.Name = _configuration["SenderName"];
            message.To.Add(MailboxAddress.Parse(email));
            message.From.Add(message.Sender);
            message.Subject = subject;

            message.Body = new TextPart(TextFormat.Html) { Text = content };

            List<Exception> errList = new();

            using (var emailClient = new SmtpClient())
            {
                try
                {
                    emailClient.Connect(_configuration["SmtpServer"], Convert.ToInt32(_configuration["SmtpPort"]), true);
                }
                catch
                {
                    errList.Add(new Exception("Something went wrong and I'm too lazy to write all the exceptions..."));
                }

                emailClient.AuthenticationMechanisms.Remove("XOAUTH2");
                emailClient.Authenticate(_configuration["SmtpUsername"], _configuration["SmtpPassword"]);

                try
                {
                    emailClient.Send(message);
                }
                catch
                {
                    errList.Add(new Exception("*BOOM* The server exploded!"));
                }

                emailClient.Disconnect(true);
            }

            return (errList.Count == 0);
        }
        #endregion

    }
}

    

