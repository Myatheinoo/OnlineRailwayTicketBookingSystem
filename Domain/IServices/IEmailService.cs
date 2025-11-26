
namespace Domain.IServices
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string htmlBody, CancellationToken ct = default);
        //Task SendEmailAsync(IEnumerable<string> toEmails, string subject, string htmlBody, CancellationToken ct = default);
    }
}
