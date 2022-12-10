using BikeSparesInventorySystem.Data.Models;
using BikeSparesInventorySystem.Data.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
