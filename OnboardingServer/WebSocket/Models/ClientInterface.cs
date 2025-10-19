namespace SignupService.WebSocket.Models;

public interface ClientInterface
{
    public Task ReceiveMessage(string message);

    public Task ReceiveVerifyEmailSent(string message);

    public Task ReceiveVerifySuccess(string message, string newPasswordLink);

    public Task ReceiveAccountCreated(string message);
}