using BikeSparesInventorySystem.Data.Models;

namespace BikeSparesInventorySystem.Data.Providers
{
    internal static class ExcelFileProviderInjection
    {
        public static IServiceCollection AddExcelFileProvider(this IServiceCollection services)
        {
            return services.AddSingleton<IFileProvider<User>, ExcelFileProvider<User>>()
                .AddSingleton<IFileProvider<Spare>, ExcelFileProvider<Spare>>()
                .AddSingleton<IFileProvider<ActivityLog>, ExcelFileProvider<ActivityLog>>();
        }
    }
}
