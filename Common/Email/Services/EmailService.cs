using Common.Email.Models;

namespace Common.Email.Services;

public abstract class EmailService
{
    public abstract Task SendEmailAsync(DataItemEmail data);
}