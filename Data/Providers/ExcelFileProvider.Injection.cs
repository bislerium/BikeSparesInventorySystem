﻿namespace BikeSparesInventorySystem.Data.Providers;

internal static class ExcelFileProviderInjection
{
    public static IServiceCollection AddExcelFileProvider(this IServiceCollection services)
    {
        return services.AddSingleton<FileProvider<User>, ExcelFileProvider<User>>()
            .AddSingleton<FileProvider<Spare>, ExcelFileProvider<Spare>>()
            .AddSingleton<FileProvider<Category>, ExcelFileProvider<Category>>()
            .AddSingleton<FileProvider<ActivityLog>, ExcelFileProvider<ActivityLog>>();
    }
}
