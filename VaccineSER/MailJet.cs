using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using VaccineSER.DTOs;
using VaccineSER.Interfaces;

namespace VaccineSER
{
    public class MailJet : IEmail
    {
        public async Task Send(string emailAddress, string subject, string body, EmailOptionsDTO options)
        {
            var client = new SmtpClient();
            client.Host = options.Host;
            client.Credentials = new NetworkCredential(options.APIKey, options.APIKeySecret);
            client.Port = options.Port;

            //options.SenderEmail = "noreply@SAHealthVaccineTracker.gov.au";
            //options.SenderEmail = "peach2010i@hotmail.com";
            var message = new MailMessage(options.SenderEmail, emailAddress, subject, body);
            //message.Subject = "Activate your account";
            //message.Body = body;
            message.IsBodyHtml = true;

            await client.SendMailAsync(message);
        }
    }
}