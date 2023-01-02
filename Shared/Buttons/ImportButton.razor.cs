using BikeSparesInventorySystem.Data.Enums;
using BikeSparesInventorySystem.Data.Models;
using BikeSparesInventorySystem.Data.Repositories;
using BikeSparesInventorySystem.Data.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace BikeSparesInventorySystem.Shared.Buttons
{
    public partial class ImportButton<T> where T : IModel
    {
        [Inject]
        private IServiceProvider ServiceProvider { get; set; }

        private async Task ImportFile(IBrowserFile file)
        {
            try
            {
                FileExtension extension;
                try
                {
                    extension = Enum.Parse<FileExtension>(file.Name.Split(".").Last());
                }
                catch
                {
                    throw new Exception("Supports JSON, CSV or Excel(.xlsx) File Only!");
                }
                Data.Providers.FileProvider<T> provider = Explorer.GetFileProvider<T>(extension);
                await ServiceProvider.GetService<Repository<T>>().ImportAsync(provider, file.OpenReadStream(Explorer.MAX_ALLOWED_IMPORT_SIZE));
                Snackbar.Add($"Imported {file.Name} File!", Severity.Success);
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
        }
    }
}
