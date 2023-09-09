using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using WebApi.Infrastructure.Data;

namespace WebApi.AuthorizarionPolicies;

public class SubscriptionPolicyService : ISubscriptionPolicyService
{
    private readonly AppDbContext _dbContext;
    private readonly IAuthorizationService _authorization;

    public SubscriptionPolicyService(AppDbContext dbContext, IAuthorizationService authorization)
    {
        _dbContext = dbContext;
        _authorization = authorization;
    }

    public async Task<AuthorizationResult> AuthorizeAsync(ClaimsPrincipal user, Guid subscriptionId)
    {
        var isActiveSubscription = _dbContext.Subscriptions.Any(s => s.IsActive && s.SubscriptionId == subscriptionId);
        var requirement = new PaidServiceRequirement(isActiveSubscription);

        return await _authorization.AuthorizeAsync(user, null, requirement);
    }
}
