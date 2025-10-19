using Common.Email.Models;
using Common.Email.Services;
using Microsoft.AspNetCore.Identity;

namespace MainServer.Common.Services;

public class EmailSender : IEmailSender<IdentityUser>
{
    private readonly EmailService _emailService;

    public EmailSender(EmailService emailService)
    {
        _emailService = emailService;
    }

    public Task SendConfirmationLinkAsync(IdentityUser user, string email, string confirmationLink)
    {
        var dataEmail = new DataItemEmail
        {
            Tos = [email],
            Body = null,
            Subject = "Verification Link",
            BodyHtml = "<p>Please click the following link to verify your account.</p><br>" + confirmationLink + "<br><br>"
        };

        return _emailService.SendEmailAsync(dataEmail);
    }

    public Task SendPasswordResetLinkAsync(IdentityUser user, string email, string resetLink)
    {
        var dataEmail = new DataItemEmail
        {
            Tos = [email],
            Body = null,
            Subject = "Password Reset Link",
            BodyHtml = "<p>Please click the following link to reset your password.</p><br>" + resetLink + "<br><br>"
        };

        return _emailService.SendEmailAsync(dataEmail);
    }

    public Task SendPasswordResetCodeAsync(IdentityUser user, string email, string resetCode)
    {
        var dataEmail = new DataItemEmail
        {
            Tos = [email],
            Body = null,
            Subject = "Password Reset Code",
            BodyHtml = "<p>Please find your password reset code below:</p><br>" + resetCode + "<br><br>"
        };

        return _emailService.SendEmailAsync(dataEmail);
    }
}