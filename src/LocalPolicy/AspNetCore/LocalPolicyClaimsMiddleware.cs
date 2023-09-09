namespace LocalPolicy.AspNetCore;

public class LocalPolicyClaimsMiddleware
{
    private readonly RequestDelegate _next;

    public LocalPolicyClaimsMiddleware(RequestDelegate next)
    {
        _next = next;
    }
  
    public async Task Invoke(HttpContext context, ILocalPolicyManager manager)
    {
        var user = context.User;
        if (user.Identity?.IsAuthenticated == true)
        {
            var policy = await manager.EvaluateAsync(user);

            var roleClaims = policy.Roles?.Select(role => new Claim(ClaimTypes.Role, role));
            var permissionClaims = policy.Permissions?.Select(permission => new Claim("permission", permission));

            var identity = new ClaimsIdentity("LocalPolicy.Middleware", "name", "role"); 
            if (roleClaims is not null)
            {
                identity.AddClaims(roleClaims);
            }   
            
            if (permissionClaims is not null)
            {
                identity.AddClaims(permissionClaims);
            }

            context.User.AddIdentity(identity);
        }

        await _next(context);
    }
}
