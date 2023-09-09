namespace LocalPolicy.Features;

public class Role
{
    public string? Name { get; set; }
    public IList<string>? IdentityRoles { get; set; } 

    internal bool EvaluateRole(ClaimsPrincipal user)
    {
        if (user is null)
        {
            throw new ArgumentNullException(nameof(user));
        }
        if (IdentityRoles is null)
        {
            return false;
        }

        var roles = user.FindAll("role").Select(x => x.Value);
        
        return roles.Any() && IdentityRoles.Any(x => roles.Contains(x));
    }
}
