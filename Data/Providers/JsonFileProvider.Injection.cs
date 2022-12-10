using BikeSparesInventorySystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeSparesInventorySystem.Data.Providers
{
    internal static class JsonFileProvider
    {
        public static IServiceCollection AddJsonFileProvider(this IServiceCollection services)
        {
            return services.AddSingleton<IFileProvider<User>, JsonFileProvider<User>>()
                .AddSingleton<IFileProvider<Spare>, JsonFileProvider<Spare>>()
                .AddSingleton<IFileProvider<ActivityLog>, JsonFileProvider<ActivityLog>>();
        }
    }
}
