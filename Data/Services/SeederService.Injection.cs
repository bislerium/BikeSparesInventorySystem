using BikeSparesInventorySystem.Data.Models;
using BikeSparesInventorySystem.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
