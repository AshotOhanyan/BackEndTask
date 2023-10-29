using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Services.StaticContent;

namespace Services.OtherServices
{
    public static class EmailService
    {
        public static async Task SendEmail(string recipient, string subject, string body)
        {
            using (MailMessage message = new MailMessage())
            {
                message.From = new MailAddress(Constants.CompanyEmail);
                message.To.Add(recipient);
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = false;

                using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com"))
                {
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(Constants.CompanyEmail, Constants.CompanyPassword);
                    smtpClient.EnableSsl = true;
                    smtpClient.Port = 587;

                    await smtpClient.SendMailAsync(message);
                }
            }
        }
    }
}
