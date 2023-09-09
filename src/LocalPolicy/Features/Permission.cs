namespace LocalPolicy.Features;

public class Permission
{
    public string? Name { get; set; }
    public IList<string>? Roles { get; set; } 

    internal bool EvaluatePermission(IEnumerable<string> roles)
    {
        if (roles == null) throw new ArgumentNullException(nameof(roles));

        if (Roles!.Any(x => roles.Contains(x))) return true;

        return false;
    }
}
