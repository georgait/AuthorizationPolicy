namespace LocalPolicy.AspNetCore.Requirements;

internal class PermissionRequirement : IAuthorizationRequirement
{
    public PermissionRequirement(string name)
    {
        Name = name;
    }

    public string Name { get; }
}