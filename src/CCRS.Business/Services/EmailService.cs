using CCRS.Business.Interfaces;
using CCRS.Business.Models;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CCRS.Business.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendHtmlEmailAsync(string email, string subject, string htmlMessage)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSettings.From),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true // Ensure the body is interpreted as HTML
            };

            mailMessage.To.Add(email);

            var smtpClient = new SmtpClient(_emailSettings.SmtpServer)
            {
                Port = _emailSettings.Port,
                Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password),
                EnableSsl = true
            };

            await smtpClient.SendMailAsync(mailMessage);
        }

        
    }
}
