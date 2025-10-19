using Common.Whatsapp.Models;

namespace Common.Whatsapp.Services;

public abstract class WhatsappService
{
    public WhatsappService(string senderPhone)
    {
        SenderPhone = senderPhone;
    }

    public string SenderPhone { get; set; }

    public abstract Task SendWhatsappAsync(DataItemWhatsapp data);
}