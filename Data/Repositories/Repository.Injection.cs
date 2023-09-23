namespace BikeSparesInventorySystem.Data.Repositories;

internal static class RepositoryInjection
{
    public static IServiceCollection AddRepository(this IServiceCollection services)
    {
        return services.AddSingleton<Repository<User>, Repository<User>>()
            .AddSingleton<Repository<Spare>, Repository<Spare>>()
            .AddSingleton<Repository<ActivityLog>, Repository<ActivityLog>>()
            .AddSingleton<Repository<Category>, Repository<Category>>()
            .AddSingleton<Repository<Miners>, Repository<Miners>>()
            .AddSingleton<Repository<Orders>, Repository<Orders>>()
            .AddSingleton<Repository<Sales>, Repository<Sales>>()
            .AddSingleton<Repository<Purchases>, Repository<Purchases>>()
            .AddSingleton<Repository<Wages>, Repository<Wages>>()
            .AddSingleton<Repository<Utilities>, Repository<Utilities>>()
            .AddSingleton<Repository<Expenses>, Repository<Expenses>>();
    }
}
