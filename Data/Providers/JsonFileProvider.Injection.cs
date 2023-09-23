namespace BikeSparesInventorySystem.Data.Providers;

internal static class JsonFileProvider
{
    public static IServiceCollection AddJsonFileProvider(this IServiceCollection services)
    {
        return services.AddSingleton<FileProvider<User>, JsonFileProvider<User>>()
            .AddSingleton<FileProvider<Spare>, JsonFileProvider<Spare>>()
            .AddSingleton<FileProvider<Category>, JsonFileProvider<Category>>()
            .AddSingleton<FileProvider<ActivityLog>, JsonFileProvider<ActivityLog>>()
            .AddSingleton<FileProvider<Miners>, JsonFileProvider<Miners>>()
            .AddSingleton<FileProvider<Orders>, JsonFileProvider<Orders>>()
            .AddSingleton<FileProvider<Sales>, JsonFileProvider<Sales>>()
            .AddSingleton<FileProvider<Purchases>, JsonFileProvider<Purchases>>()
            .AddSingleton<FileProvider<Wages>, JsonFileProvider<Wages>>()
            .AddSingleton<FileProvider<Utilities>, JsonFileProvider<Utilities>>()
            .AddSingleton<FileProvider<Expenses>, JsonFileProvider<Expenses>>();
    }
}
