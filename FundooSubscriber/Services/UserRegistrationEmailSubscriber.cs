using FundooSubscriber.Models;
using MassTransit;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FundooSubscriber.Services
{
    public class UserRegistrationEmailSubscriber : IConsumer<UserRegistrationMesssage>
    {
        public async Task Consume(ConsumeContext<UserRegistrationMesssage> context)
        {
            var userRegistrationMessage = context.Message;

            await SendWelcomeEmail(userRegistrationMessage.Email);
        }
        private async Task SendWelcomeEmail(string Email)
        {
            try
            {
                using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com"))
                {
                    smtpClient.Port = 587;
                    smtpClient.Credentials = new NetworkCredential("yashwanth.k1119@gmail.com", "krmdzqgryywqthlj");
                    smtpClient.EnableSsl = true;
                }

                using (MailMessage mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress("yashwanth.k1119@gmail.com");
                    mailMessage.To.Add(Email);
                    mailMessage.Subject = "Welcome to our App";
                    mailMessage.Body = "Thank You For Registring.Welcome to our app!";
                    mailMessage.IsBodyHtml = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
