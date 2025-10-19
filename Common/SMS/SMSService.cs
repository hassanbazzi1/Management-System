namespace Common.SMS;

public abstract class SmsService
{
    public SmsService(string senderPhone)
    {
        SenderPhone = senderPhone;
    }

    public string SenderPhone { get; set; }

    public abstract Task SendSmsAsync(DataItemPhone email);
}