using Microsoft.AspNetCore.Authorization;

namespace WebApi.AuthorizarionPolicies;

public class PaidServiceRequirementHandler : AuthorizationHandler<PaidServiceRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PaidServiceRequirement requirement)
    {
        if (requirement.IsActiveSubscription)
        {
            context.Succeed(requirement);
        }
        return Task.CompletedTask;
    }
}
