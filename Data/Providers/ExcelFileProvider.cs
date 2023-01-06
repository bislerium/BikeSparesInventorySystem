namespace BikeSparesInventorySystem.Data.Providers;

internal class ExcelFileProvider<TSource> : FileProvider<TSource> where TSource : IModel
{
    internal override string FilePath { get; set; } = Explorer.GetDefaultFilePath<TSource>(Enums.FileExtension.xlsx);

    internal override async Task<ICollection<TSource>> ReadAsync(string path)
    {
        return await ReadAsync(File.OpenRead(path));
    }

    internal override async Task<ICollection<TSource>> ReadAsync(Stream stream)
    {
        try
        {
            return (await new ExcelMapper().FetchAsync<TSource>(stream)).ToList();
        }
        catch
        {
            throw;
        }
        finally
        {
            stream.Close();
        }
    }

    internal override async Task WriteAsync(string path, ICollection<TSource> data)
    {
        await new ExcelMapper().SaveAsync(path, data, typeof(TSource).Name);
    }
}
