namespace LocalPolicy.Features;

public class Policy
{
    public IList<Role>? Roles { get; set; }
    public IList<Permission>? Permissions { get; set; }

    internal Task<PolicyResult> EvaluateAsync(ClaimsPrincipal user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        var roles = Roles?.Where(r => r.EvaluateRole(user)).Select(r => r.Name).ToArray();
        var permissions = Permissions?.Where(p => p.EvaluatePermission(roles!)).Select(p => p.Name).ToArray();

        var result = new PolicyResult()
        {
            Roles = roles?.Distinct()!,
            Permissions = permissions?.Distinct()!
        };

        return Task.FromResult(result);
    }
}
