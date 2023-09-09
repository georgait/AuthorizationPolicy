namespace LocalPolicy.AspNetCore.Extensions;

public static class ServiceCollectionExtensions
{
    public static LocalPolicyBuilder AddLocalAuthorizationPolicy(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<Policy>(opt =>
        {
            opt.Permissions = configuration.GetSection("Authorization:LocalPolicy:permissions").Get<List<Permission>>();
            opt.Roles = configuration.GetSection("Authorization:LocalPolicy:roles").Get<List<Role>>();
        });
        services.AddTransient<ILocalPolicyManager, LocalPolicyManager>();
        services.AddScoped(provider => provider.GetRequiredService<IOptionsSnapshot<Policy>>().Value);

        return new LocalPolicyBuilder(services);
    }
}
