namespace MainServer.Features.DemoSplitter.Models;

public class WhatsAppWebhookPayload
{
    public string Object { get; set; }
    public List<Entry> Entries { get; set; }
}

public class Entry
{
    public string Id { get; set; }
    public List<Change> Changes { get; set; }
}

public class Change
{
    public ChangeValue Value { get; set; }
    public string Field { get; set; }
}

public class ChangeValue
{
    public string MessagingProduct { get; set; }
    public Metadata Metadata { get; set; }
    public List<Contact> Contacts { get; set; }
    public List<Message> Messages { get; set; }
}

public class Metadata
{
    public string DisplayPhoneNumber { get; set; }
    public string PhoneNumberId { get; set; }
}

public class Contact
{
    public Profile Profile { get; set; }
    public string WaId { get; set; }
}

public class Profile
{
    public string Name { get; set; }
}

public class Message
{
    public string From { get; set; }
    public string Id { get; set; }
    public string Timestamp { get; set; }
    public MessageText Text { get; set; }
    public string Type { get; set; }
}

public class MessageText
{
    public string Body { get; set; }
}