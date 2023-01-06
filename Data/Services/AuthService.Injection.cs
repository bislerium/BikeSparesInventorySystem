namespace BikeSparesInventorySystem.Data.Services;

internal static class AuthServiceInjection
{
    public static IServiceCollection AddAuth(this IServiceCollection services)
    {
        return services.AddSingleton<AuthService>();
    }
}
