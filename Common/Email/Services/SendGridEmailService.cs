using Common.Email.Models;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Common.Email.Services;

public class SendGridEmailService : EmailService
{
    private IOptionsMonitor<ExternalEmailOptions> _options;
    
    public SendGridEmailService(IOptionsMonitor<ExternalEmailOptions> options)
    {
        _options = options;
    }

    private string ApiKey { get; }

    public override async Task SendEmailAsync(DataItemEmail data)
    {
        var client = new SendGridClient(ApiKey);
        var from = new EmailAddress(_options.CurrentValue.SenderEmail, _options.CurrentValue.SenderEmail);
        var to = new EmailAddress(data.Tos.First(), data.Tos.First());
        var msg = MailHelper.CreateSingleEmail(from, to, data.Subject, data.Body, data.BodyHtml);
        await client.SendEmailAsync(msg);
    }
}