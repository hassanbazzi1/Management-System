using Common.API.ApiMapping.Subscriptions.Models;
using Common.DB.ClientDB.Models;
using Riok.Mapperly.Abstractions;

namespace Common.API.ApiMapping.Subscriptions.Mappers;

[Mapper]
public partial class SubscriptionToApiMapper
{
    public partial SubscriptionResponse MapNew(Subscription subscription);
}