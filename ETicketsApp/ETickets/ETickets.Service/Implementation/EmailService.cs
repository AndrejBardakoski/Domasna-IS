using ETickets.Domain;
using ETickets.Domain.DTO;
using ETickets.Service.Interface;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ETickets.Service.Implementation
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;

        public EmailService(EmailSettings settings)
        {
            _settings = settings;
        }

        public async Task SendEmailAsync(EmailDTO eMail)
        {
            var emailMimeMessage = new MimeMessage
            {
                Sender = new MailboxAddress(_settings.SendersName, _settings.SmtpUserName),
                Subject = eMail.Subject
            };

            emailMimeMessage.From.Add(new MailboxAddress(_settings.EmailDisplayName, _settings.SmtpUserName));

            emailMimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Plain) { Text = eMail.Content };

            emailMimeMessage.To.Add(new MailboxAddress(eMail.MailTo, eMail.MailTo));

            try
            {
                using (var smtp = new MailKit.Net.Smtp.SmtpClient())
                {
                    var socketOptions = _settings.EnableSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.Auto;

                    await smtp.ConnectAsync(_settings.SmtpServer, _settings.SmtpServerPort, socketOptions);

                    if (!string.IsNullOrEmpty(_settings.SmtpUserName))
                    {
                        await smtp.AuthenticateAsync(_settings.SmtpUserName, _settings.SmtpPassword);
                    }
                    await smtp.SendAsync(emailMimeMessage);

                    await smtp.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
