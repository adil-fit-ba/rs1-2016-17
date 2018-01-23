using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;

namespace eAkademija.Data.Services
{
    public class EmailService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            await sendEmailAsync(message);
        }
        private async Task sendEmailAsync(IdentityMessage message)
        {
            string serviceMail = "";
            string serviceMailPass = "";
            Debug.WriteLine("Email: " + message.Destination);
            MailMessage mailMessage = new MailMessage(serviceMail, message.Destination);
            mailMessage.Body = message.Body;
            mailMessage.Subject = message.Subject;
            mailMessage.IsBodyHtml = true;

            using (SmtpClient client = new SmtpClient())
            {
                client.Port = 587;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(serviceMail, serviceMailPass);
                await client.SendMailAsync(mailMessage);
            }
        }
    }
}
