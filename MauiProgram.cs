using BikeSparesInventorySystem.Data.Providers;
using BikeSparesInventorySystem.Data.Repositories;
using BikeSparesInventorySystem.Data.Services;
using MudBlazor.Services;

namespace BikeSparesInventorySystem;

public static class MauiProgram
{
	public static MauiApp CreateMauiAppAsync()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddMauiBlazorWebView();

		#if DEBUG
			builder.Services.AddBlazorWebViewDeveloperTools();
		#endif

        builder.Services.AddMudServices();

        builder.Services.AddCsvFileProvider();
        // builder.Services.AddExcelFileProvider();
        // builder.Services.AddJsonFileProvider();

        builder.Services.AddRepository();

        builder.Services.AddSeeder();

        builder.Services.AddAuth();

		return  builder.Build();
	}
}
