namespace LocalPolicy.AspNetCore.Builders;

public class LocalPolicyBuilder
{
    public LocalPolicyBuilder(IServiceCollection services)
    {
        Services = services;
    }

    public IServiceCollection Services { get; }

    public LocalPolicyBuilder AddPermissionPolicies()
    {
        Services.AddAuthorizationCore();
        Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        Services.AddTransient<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();
        Services.AddTransient<IAuthorizationHandler, PermissionRequirementHandler>();

        return this;
    }
}
