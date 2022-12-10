using BikeSparesInventorySystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeSparesInventorySystem.Data.Providers
{
    internal static class CSVFileProviderInjection
    {
        public static IServiceCollection AddCsvFileProvider(this IServiceCollection services)
        {
            return services.AddSingleton<IFileProvider<User>, CsvFileProvider<User>>()
                .AddSingleton<IFileProvider<Spare>, CsvFileProvider<Spare>>()
                .AddSingleton<IFileProvider<ActivityLog>, CsvFileProvider<ActivityLog>>();
        }
    }
}
