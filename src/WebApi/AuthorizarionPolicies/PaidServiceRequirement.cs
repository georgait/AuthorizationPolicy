using Microsoft.AspNetCore.Authorization;

namespace WebApi.AuthorizarionPolicies;

public class PaidServiceRequirement : IAuthorizationRequirement
{
    public PaidServiceRequirement(bool isActiveSubscription)
    {
        IsActiveSubscription = isActiveSubscription;
    }

    public bool IsActiveSubscription { get; }
}
