namespace BikeSparesInventorySystem.Data.Services
{
    internal static class SeederServiceInjection
    {
        public static IServiceCollection AddSeeder(this IServiceCollection services)
        {
            return services.AddSingleton<SeederService>();
        }
    }
}
