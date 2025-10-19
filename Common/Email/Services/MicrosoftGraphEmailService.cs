using Azure.Identity;
using Common.Email.Models;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.Graph.Users.Item.SendMail;

namespace Common.Email.Services;

public class MicrosoftGraphEmailService : EmailService
{
    private readonly IOptionsMonitor<MicrosoftEmailOptions> _options;

    public MicrosoftGraphEmailService(IOptionsMonitor<MicrosoftEmailOptions> options)
    {
        _options = options;
    }

    public override async Task SendEmailAsync(DataItemEmail data)
    {
        await SendEmailMicrosoftAsync(data);
    }

    public Message BuildEmail(DataItemEmail data)
    {
        var message = new Message
        {
            Subject = data.Subject
        };

        message.ToRecipients = new List<Recipient>();
        foreach (var recipient in data.Tos)
            message.ToRecipients.Add(new Recipient
            {
                EmailAddress = new EmailAddress
                {
                    Address = recipient
                }
            });

        if (data.BodyHtml == null)
            message.Body = new ItemBody
            {
                ContentType = BodyType.Text,
                Content = data.Body
            };
        else
            message.Body = new ItemBody
            {
                ContentType = BodyType.Html,
                Content = data.BodyHtml
            };

        return message;
    }

    public async Task SendEmailMicrosoftAsync(DataItemEmail data)
    {
        var email = BuildEmail(data);
        var requestBody = new SendMailPostRequestBody
        {
            Message = email,
            SaveToSentItems = false
        };

        var options = new TokenCredentialOptions
        {
            AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
        };

        var credential = new ClientSecretCredential(_options.CurrentValue.TenantId, _options.CurrentValue.ClientId, _options.CurrentValue.ClientSecret, options);
        var graphClient = new GraphServiceClient(credential, _options.CurrentValue.Scopes);
        //var token = await credential.GetTokenAsync(new TokenRequestContext(MicrosoftAuth.Scopes));
        //Console.WriteLine($"Access token: {token.Token}");
        await graphClient.Users[_options.CurrentValue.UserId].SendMail.PostAsync(requestBody);
    }
}