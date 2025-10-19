//using Twilio;
//using Twilio.Rest.Api.V2010.Account;

namespace Common.SMS;

public class TwilioSmsService : SmsService
{
    public TwilioSmsService(string accountId, string authToken, string senderPhone) : base(senderPhone)
    {
        AccountId = accountId;
        AuthToken = authToken;
    }

    public string AccountId { get; set; }
    public string AuthToken { get; set; }

    public override async Task SendSmsAsync(DataItemPhone data)
    {
        /*
        TwilioClient.Init(AccountID, AuthToken);

        await MessageResource.CreateAsync(
            body: data.Body,
            from: new Twilio.Types.PhoneNumber(SenderPhone),
            to: new Twilio.Types.PhoneNumber(data.ToPhone));*/
    }
}