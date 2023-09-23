namespace BikeSparesInventorySystem.Data.Providers;

internal static class CSVFileProviderInjection
{
    public static IServiceCollection AddCsvFileProvider(this IServiceCollection services)
    {
        return services.AddSingleton<FileProvider<User>, CsvFileProvider<User>>()
            .AddSingleton<FileProvider<Spare>, CsvFileProvider<Spare>>()
            .AddSingleton<FileProvider<ActivityLog>, CsvFileProvider<ActivityLog>>()
            .AddSingleton<FileProvider<Category>, CsvFileProvider<Category>>()
            .AddSingleton<FileProvider<Miners>, CsvFileProvider<Miners>>()
            .AddSingleton<FileProvider<Orders>, CsvFileProvider<Orders>>()
            .AddSingleton<FileProvider<Sales>, CsvFileProvider<Sales>>()
            .AddSingleton<FileProvider<Purchases>, CsvFileProvider<Purchases>>()
            .AddSingleton<FileProvider<Wages>, CsvFileProvider<Wages>>()
            .AddSingleton<FileProvider<Utilities>, CsvFileProvider<Utilities>>()
            .AddSingleton<FileProvider<Expenses>, CsvFileProvider<Expenses>>();
    }
}
