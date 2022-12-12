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
            return services.AddSingleton<FileProvider<User>, CsvFileProvider<User>>()
                .AddSingleton<FileProvider<Spare>, CsvFileProvider<Spare>>()
                .AddSingleton<FileProvider<ActivityLog>, CsvFileProvider<ActivityLog>>();
        }
    }
}
