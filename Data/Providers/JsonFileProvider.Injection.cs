﻿namespace BikeSparesInventorySystem.Data.Providers;

internal static class JsonFileProvider
{
    public static IServiceCollection AddJsonFileProvider(this IServiceCollection services)
    {
        return services.AddSingleton<FileProvider<User>, JsonFileProvider<User>>()
            .AddSingleton<FileProvider<Spare>, JsonFileProvider<Spare>>()
            .AddSingleton<FileProvider<Category>, JsonFileProvider<Category>>()
            .AddSingleton<FileProvider<ActivityLog>, JsonFileProvider<ActivityLog>>();
    }
}
