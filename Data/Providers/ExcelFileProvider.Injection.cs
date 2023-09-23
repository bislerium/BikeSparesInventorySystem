namespace BikeSparesInventorySystem.Data.Providers;

internal static class ExcelFileProviderInjection
{
    public static IServiceCollection AddExcelFileProvider(this IServiceCollection services)
    {
        return services.AddSingleton<FileProvider<User>, ExcelFileProvider<User>>()
            .AddSingleton<FileProvider<Spare>, ExcelFileProvider<Spare>>()
            .AddSingleton<FileProvider<Category>, ExcelFileProvider<Category>>()
            .AddSingleton<FileProvider<ActivityLog>, ExcelFileProvider<ActivityLog>>()
            .AddSingleton<FileProvider<Miners>, ExcelFileProvider<Miners>>()
            .AddSingleton<FileProvider<Orders>, ExcelFileProvider<Orders>>()
            .AddSingleton<FileProvider<Sales>, ExcelFileProvider<Sales>>()
            .AddSingleton<FileProvider<Purchases>, ExcelFileProvider<Purchases>>()
            .AddSingleton<FileProvider<Wages>, ExcelFileProvider<Wages>>()
            .AddSingleton<FileProvider<Utilities>, ExcelFileProvider<Utilities>>()
            .AddSingleton<FileProvider<Expenses>, ExcelFileProvider<Expenses>>();
    }
}
