namespace LocalPolicy.AspNetCore.Requirements;

internal class PermissionRequirementHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly ILocalPolicyManager _manager;

    public PermissionRequirementHandler(ILocalPolicyManager manager)
    {
        _manager = manager;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        if((bool) await _manager.HasPermissionAsync(context.User, requirement.Name))
        {
            context.Succeed(requirement);
        }
    }
}