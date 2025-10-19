using Common.Whatsapp.Models;
using WhatsappBusiness.CloudApi;
using WhatsappBusiness.CloudApi.Configurations;
using WhatsappBusiness.CloudApi.Messages.Requests;

namespace Common.Whatsapp.Services;

public class WhatsappBusinessApiService : WhatsappService
{
    public WhatsappBusinessApiService(string accessToken, string whatsappPhoneNumberId, string senderPhone) : base(senderPhone)
    {
        AccessToken = accessToken;
        WhatsappPhoneNumberId = whatsappPhoneNumberId;
    }

    public string AccessToken { get; set; }

    public string WhatsappPhoneNumberId { get; set; }

    public WhatsAppBusinessClient GetWhatsAppBusinessClient()
    {
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("https://graph.facebook.com/v22.0/");
        //httpClient.BaseAddress = WhatsAppBusinessRequestEndpoint.BaseAddress;

        var config = new WhatsAppBusinessCloudApiConfig
        {
            AccessToken = AccessToken,
            WhatsAppBusinessPhoneNumberId = WhatsappPhoneNumberId
        };
        var whatsAppBusinessClient = new WhatsAppBusinessClient(httpClient, config);

        return whatsAppBusinessClient;
    }

    public override async Task SendWhatsappAsync(DataItemWhatsapp data)
    {
        var textMessageRequest = new TextMessageRequest();
        textMessageRequest.To = data.ToPhone;
        var text = new WhatsAppText();
        text.Body = data.Body;

        textMessageRequest.Text = text;

        var client = GetWhatsAppBusinessClient();
        var response = await client.SendTextMessageAsync(textMessageRequest);
        Console.WriteLine(response);
    }
}