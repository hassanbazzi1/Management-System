using System.Security.Cryptography.X509Certificates;
using System.Text;
using Common.Email.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Common.Email.Services;

public class GmailApiEmailService : EmailService
{
    private readonly IOptionsMonitor<GoogleEmailOptions> _options;

    public GmailApiEmailService(IOptionsMonitor<GoogleEmailOptions> options)
    {
        _options = options;
    }

    public override async Task SendEmailAsync(DataItemEmail data)
    {
        if (_options != null)
            await SendEmailGoogleAsync(data);
    }

    public MimeMessage BuildEmail(DataItemEmail data)
    {
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress("Sender", _options.CurrentValue.SenderEmail));
        foreach (var address in data.Tos)
            email.To.Add(new MailboxAddress("Recipient", address));

        email.Subject = data.Subject;

        var builder = new BodyBuilder();
        builder.TextBody = data.Body;
        builder.HtmlBody = data.BodyHtml;

        email.Body = builder.ToMessageBody();
        return email;
    }

    public async Task SendEmailGoogleAsync(DataItemEmail data)
    {
        var email = BuildEmail(data);

        var gmailMessage = new Message
        {
            Raw = EncodeBase64Url(email.ToString())
        };

        // TODO: Make relative URL work
        var certificate = new X509Certificate2(@"C:\Users\samij\google-certificate.p12", _options.CurrentValue.CertificatePassword, X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable | X509KeyStorageFlags.PersistKeySet);

        var credential = new ServiceAccountCredential(new ServiceAccountCredential.Initializer(_options.CurrentValue.ServiceAccountEmail)
        {
            Scopes = _options.CurrentValue.Scopes,
            User = _options.CurrentValue.Username
        }.FromCertificate(certificate));

        var requestAccessToken = await credential.RequestAccessTokenAsync(CancellationToken.None);
        if (!requestAccessToken)
            throw new InvalidOperationException("Access token failed.");

        var service = new GmailService(new BaseClientService.Initializer
        {
            HttpClientInitializer = credential
        });

        var sendRequest = service.Users.Messages.Send(gmailMessage, "me");
        await sendRequest.ExecuteAsync();
    }

    private static string EncodeBase64Url(string input)
    {
        var inputBytes = Encoding.UTF8.GetBytes(input);
        return Convert.ToBase64String(inputBytes).Replace('+', '-').Replace('/', '_').Replace("=", "");
    }
}