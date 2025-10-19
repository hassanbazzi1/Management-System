using Microsoft.AspNetCore.Mvc;

namespace Common.API.Models;

public class ApiController<T> : ControllerBase
{
    // Common Parameter Names
    protected const string ParamToken = "Token";
    protected const string ParamFromChat = "FromChat";

    // Common Claims
    protected const string ClaimsPhone = "phone";
    protected const string ClaimsData = "data";
    protected const string ClaimsClientId = "clientID";
    protected const string ClaimsSubscriptionType = "subscriptionType";
    protected const string ClaimsUserId = "userID";
    protected const string ClaimsLink = "link";
    protected const string ClaimsStatus = "status";
    protected readonly T DbContext;

    public ApiController(T dbContext)
    {
        DbContext = dbContext;
    }
}