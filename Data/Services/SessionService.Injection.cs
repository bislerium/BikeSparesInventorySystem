namespace BikeSparesInventorySystem.Data.Services;

internal static class SessionServiceInjection
{
	public static IServiceCollection AddSession(this IServiceCollection services)
	{
		return services.AddSingleton<SessionService>();
	}
}
