using Domain.Configurations;
using Domain.IServices;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Microsoft.Extensions.Options;

namespace Domain.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;

        public EmailService(IOptions<EmailSettings> options)
        {
            _settings = options.Value;
        }

        private MimeMessage CreateMessage(string to, string subject, string htmlBody)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_settings.FromName, _settings.FromAddress));
            message.To.Add(MailboxAddress.Parse(to));
            message.Subject = subject;
            message.Body = new BodyBuilder { HtmlBody = htmlBody }.ToMessageBody();
            return message;
        }

        public async Task SendEmailAsync(string to, string subject, string htmlBody, CancellationToken ct = default)
        {
            var message = CreateMessage(to, subject, htmlBody);
            await SendAsync(message, ct);
        }

        private async Task SendAsync(MimeMessage message, CancellationToken ct)
        {
            using var client = new SmtpClient();

            try
            {
                var socketOption = _settings.UseStartTls
                    ? SecureSocketOptions.StartTls
                    : SecureSocketOptions.Auto;

                await client.ConnectAsync(_settings.SmtpHost, _settings.SmtpPort, socketOption, ct);

                if (!string.IsNullOrWhiteSpace(_settings.UserName))
                {
                    await client.AuthenticateAsync(_settings.UserName, _settings.Password, ct);
                }

                await client.SendAsync(message, ct);
                await client.DisconnectAsync(true, ct);
            }
            catch (Exception ex)
            {
                throw; // rethrow for higher-level handling
            }
        }
    }
}
