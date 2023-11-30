namespace BikeSparesInventorySystem.Components.Buttons;

public partial class ImportButton<T> where T : IModel
{
    [Inject]
    private IServiceProvider ServiceProvider { get; set; }

    [Parameter] public Action ChangeParentState { get; set; }

    private bool Append;

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
            FileProvider<T> provider = Explorer.GetFileProvider<T>(extension);
            await ServiceProvider.GetService<Repository<T>>().ImportAsync(provider, file.OpenReadStream(Explorer.MAX_ALLOWED_IMPORT_SIZE), Append);
            ChangeParentState.Invoke();

            Snackbar.Add($"Imported {file.Name} File!", Severity.Success);
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }
}
