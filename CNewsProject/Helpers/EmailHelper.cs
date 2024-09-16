using MimeKit;
using MailKit.Net.Smtp;


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

        //ROBERTS WAY. VI FÅR SE

        #region ROBERTS WAY. VI FÅR SE

        public bool SendEmailAsync(string email, string subject, string link, string desc)
        {
            var message = new MimeMessage();
            message.Sender = MailboxAddress.Parse(_configuration["SenderEmail"]);
            message.Sender.Name = _configuration["SenderName"];
            message.To.Add(MailboxAddress.Parse(email));
            message.From.Add(message.Sender);
            message.Subject = subject;
            
            var htmlBuild = new BodyBuilder
            {
                HtmlBody = $@"
                <html>
                    <body>
                        <p>{desc}</p>
                        <p>Please click <a href=""{link}"">here.</a></p>
                    </body>
                </html>"
            };
            
            message.Body = htmlBuild.ToMessageBody();

            List<Exception> errList = new();

            using (var emailClient = new SmtpClient())
            {
                try
                {
                    emailClient.Connect(_configuration["SmtpServer"], Convert.ToInt32(_configuration["SmtpPort"]),
                        true);
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