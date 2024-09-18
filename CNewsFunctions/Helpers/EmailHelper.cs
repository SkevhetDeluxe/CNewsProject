using CNewsFunctions.Models;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.Extensions.Configuration;

//using System.Net.Mail;

namespace CNewsProject.Helpers
{
    public class EmailHelper(IConfiguration config)
    {
        //ROBERTS WAY. VI FÅR SE

        #region ROBERTS WAY. VI FÅR SE

        public bool SendEmailAsync(string email, string subject, string link, string desc)
        {
            var message = new MimeMessage();
            message.Sender = MailboxAddress.Parse(config["SenderEmail"]);
            message.Sender.Name = config["SenderName"];
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
                    emailClient.Connect(config["SmtpServer"], Convert.ToInt32(config["SmtpPort"]),
                        true);
                }
                catch
                {
                    errList.Add(new Exception("Something went wrong and I'm too lazy to write all the exceptions..."));
                }

                emailClient.AuthenticationMechanisms.Remove("XOAUTH2");
                emailClient.Authenticate(config["SmtpUsername"], config["SmtpPassword"]);

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

        public bool SendEmailInstructionAsync(EmailInstruction instruction)
        {
            var message = new MimeMessage();
            message.Sender = MailboxAddress.Parse(config["SenderEmail"]);
            message.Sender.Name = config["SenderName"];
            message.To.Add(MailboxAddress.Parse(instruction.Email));
            message.From.Add(message.Sender);
            message.Subject = instruction.Subject;


            var htmlBuild = new BodyBuilder
            {
                HtmlBody = $@"
                <html>
                    <body>
                        <p>{instruction.Subject}</p>
                        <p>JUST TESTING MATE</P>
                    </body>
                </html>"
            };
            
            message.Body = htmlBuild.ToMessageBody();

            List<Exception> errList = new();

            using (var emailClient = new SmtpClient())
            {
                try
                {
                    emailClient.Connect(config["SmtpServer"], Convert.ToInt32(config["SmtpPort"]),
                        true);
                }
                catch
                {
                    errList.Add(new Exception("Something went wrong and I'm too lazy to write all the exceptions..."));
                }

                emailClient.AuthenticationMechanisms.Remove("XOAUTH2");
                emailClient.Authenticate(config["SmtpUsername"], config["SmtpPassword"]);

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
        
        #region Constructing HTML

        public string ConstructHtmlBody(string message)
        {

            return "";
        }
        
        #endregion
    }
}