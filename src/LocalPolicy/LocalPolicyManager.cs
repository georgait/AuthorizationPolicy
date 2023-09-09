namespace LocalPolicy;

public class LocalPolicyManager : ILocalPolicyManager
{
    private readonly Policy _policy;

    public LocalPolicyManager(Policy policy)
    {
        _policy = policy;
    }

    public Task<PolicyResult> EvaluateAsync(ClaimsPrincipal user)
    {
        if (user is null) throw new ArgumentNullException(nameof(user));
        return _policy.EvaluateAsync(user);
    }

    public async Task<bool?> HasPermissionAsync(ClaimsPrincipal user, string permission)
    {
        var policy = await EvaluateAsync(user);
        return policy.Permissions?.Contains(permission);
    }

    public async Task<bool?> IsInRoleAsync(ClaimsPrincipal user, string role)
    {
        var policy = await EvaluateAsync(user);
        return policy.Roles?.Contains(role);
    }
}
