using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace WebApi.AuthorizarionPolicies;

public interface ISubscriptionPolicyService
{
    Task<AuthorizationResult> AuthorizeAsync(ClaimsPrincipal user, Guid subscriptionId);
}
