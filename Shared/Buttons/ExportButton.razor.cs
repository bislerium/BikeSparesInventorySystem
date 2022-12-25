using BikeSparesInventorySystem.Data.Enums;
using BikeSparesInventorySystem.Data.Models;
using BikeSparesInventorySystem.Data.Repositories;
using BikeSparesInventorySystem.Data.Utils;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BikeSparesInventorySystem.Shared.Buttons;

public partial class ExportButton<T> where T : IModel
{
    [Inject]
    private IServiceProvider ServiceProvider { get; set; }

    public async Task ExportFile(FileExtension extension)
    {
        var repo = ServiceProvider.GetService<Repository<T>>();
        if (repo.Count == 0)
        {
            Snackbar.Add($"Cannot Export Empty Data!", Severity.Error);
            return;
        }
        try
        {
            //await repo.FlushAsync(Explorer.GetDefaultExportFilePath<T>(extension));

            string filePath = Explorer.GetFilePath("D:", Explorer.GetFile<T>(extension));
            var provider = Explorer.GetFileProvider<T>(extension);
            await repo.ExportAsync(provider, filePath);
            Snackbar.Add($"Exported to {filePath}!", Severity.Success);
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }

}
