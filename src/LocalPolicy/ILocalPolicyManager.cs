namespace LocalPolicy;

public interface ILocalPolicyManager
{
    Task<PolicyResult> EvaluateAsync(ClaimsPrincipal user);
    Task<bool?> HasPermissionAsync(ClaimsPrincipal user, string permission);
    Task<bool?> IsInRoleAsync(ClaimsPrincipal user, string role);
}
