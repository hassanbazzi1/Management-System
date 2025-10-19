using Common.Whatsapp.Models;

//using Twilio;
//using Twilio.Rest.Api.V2010.Account;

namespace Common.Whatsapp.Services;

public class TwilioWhatsappService : WhatsappService
{
    public TwilioWhatsappService(string accountId, string authToken, string senderPhone) : base(senderPhone)
    {
        AccountId = accountId;
        AuthToken = authToken;
    }

    public string AccountId { get; set; }
    public string AuthToken { get; set; }

    public override async Task SendWhatsappAsync(DataItemWhatsapp data)
    {
        /*
        TwilioClient.Init(AccountID, AuthToken);

        await MessageResource.CreateAsync(
            body: data.Body,
            from: new Twilio.Types.PhoneNumber("whatsapp:" + SenderPhone),
            to: new Twilio.Types.PhoneNumber("whatsapp:" + data.ToPhone));*/
    }
}