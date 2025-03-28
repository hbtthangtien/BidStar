using Application.Interface.IExternalService;
using Domain.Configs;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Infrustucture.ExternalService
{
    public class EmailService : ISenderService
    {
        private readonly EmailConfig _emailConfig;

        public EmailService(IOptions<EmailConfig> options)
        {
            _emailConfig = options.Value;
            Console.WriteLine($"📧 [Debug] EmailService config: Host={_emailConfig.Host}, User={_emailConfig.Username}");
        }
        public async Task SendMailAsync(string Subject, string Body, string to)
        {
            Console.WriteLine("📧 Sending mail...");
            Console.WriteLine($"To: {to}, Subject: {Subject}");
            Console.WriteLine($"SMTP: {_emailConfig.Host}:{_emailConfig.Port}, User: {_emailConfig.Username}");
            var smtpClient = new SmtpClient
            {
                Host = _emailConfig.Host,
                Port = int.Parse(_emailConfig.Port),
                Credentials = new System.Net.NetworkCredential(
                _emailConfig.Username, _emailConfig.Password),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailConfig.From),
                Subject = Subject,
                Body = Body,
                IsBodyHtml = true
            };
            mailMessage.To.Add(to);
            await smtpClient.SendMailAsync(mailMessage);

        }

    }
}
