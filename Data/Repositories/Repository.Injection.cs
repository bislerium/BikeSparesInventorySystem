using BikeSparesInventorySystem.Data.Models;

namespace BikeSparesInventorySystem.Data.Repositories
{
    internal static class RepositoryInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            return services.AddSingleton<IRepository<User>, Repository<User>>()
                .AddSingleton<IRepository<Spare>, Repository<Spare>>()
                .AddSingleton<IRepository<ActivityLog>, Repository<ActivityLog>>();
        }
    }
}
