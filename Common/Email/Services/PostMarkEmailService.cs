using Common.Email.Models;
using Microsoft.Extensions.Options;
using PostmarkDotNet;

namespace Common.Email.Services;

public class PostMarkEmailService : EmailService
{
    private IOptionsMonitor<ExternalEmailOptions> _options;
    
    public PostMarkEmailService(IOptionsMonitor<ExternalEmailOptions> options)
    {
        _options = options;
    }
    
    public PostmarkMessage BuildEmail(DataItemEmail data)
    {
        // Send an email asynchronously:
        var message = new PostmarkMessage
        {
            To = data.Tos.First(),
            From = _options.CurrentValue.SenderEmail,
            TrackOpens = true,
            Subject = data.Subject,
            TextBody = data.Body,
            HtmlBody = data.BodyHtml,
            Tag = data.Subject
        };

        return message;
    }

    public override async Task SendEmailAsync(DataItemEmail data)
    {
        var message = BuildEmail(data);
        var client = new PostmarkClient(_options.CurrentValue.ApiToken);
        var sendResult = await client.SendMessageAsync(message);

        if (sendResult.Status == PostmarkStatus.Success)
        {
        }
        else
        {
            throw new Exception(sendResult.Message);
        }
    }
}