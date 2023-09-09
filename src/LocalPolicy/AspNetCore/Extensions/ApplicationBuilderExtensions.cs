namespace LocalPolicy.AspNetCore.Extensions;

public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Makes roles and permissions available as claims.
    /// Use this middleware for "IsInRole" functionality and "Authorize[Roles="role"]" attribute.
    /// This is not needed when using the `LocalPolicy` library directly or the policy-based authorization (ASP.NET Core)
    /// </summary>
    /// <param name="context"></param>
    /// <param name="manager"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseLocalPolicyClaims(this IApplicationBuilder app) =>
        app.UseMiddleware<LocalPolicyClaimsMiddleware>();
}
