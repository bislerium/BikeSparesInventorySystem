using BikeSparesInventorySystem.Data.Enums;
using BikeSparesInventorySystem.Data.Models;
using BikeSparesInventorySystem.Data.Repositories;
using BikeSparesInventorySystem.Data.Utils;
using MudBlazor;

namespace BikeSparesInventorySystem.Shared.Buttons;

public partial class ExportButton<T> where T : IModel
{

    public async Task ExportFile(FileExtension extension)
    {
        Repository<T> repo = ServiceProvider.GetService<Repository<T>>();
        if (repo.Count == 0)
        {
            Snackbar.Add($"Cannot Export Empty Data!", Severity.Error);
            return;
        }
        try
        {
            string filePath = Explorer.GetDefaultExportFilePath<T>(extension);
            Data.Providers.FileProvider<T> provider = Explorer.GetFileProvider<T>(extension);
            await repo.ExportAsync(provider, filePath);

            Snackbar.Add($"Exported to {filePath}!", Severity.Info);
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }

}
