using BikeSparesInventorySystem.Data.Models;
using BikeSparesInventorySystem.Data.Utils;
using System.Text.Json;

namespace BikeSparesInventorySystem.Data.Providers;

internal class JsonFileProvider<TSource> : FileProvider<TSource> where TSource : IModel
{
    internal override string FilePath { get; set; } = Explorer.GetDefaultFilePath<TSource>(Enums.FileExtension.json);

    internal override async Task<ICollection<TSource>> ReadAsync(string path) => await ReadAsync(File.OpenRead(path));

    internal override async Task<ICollection<TSource>> ReadAsync(Stream stream)
    {
        try
        {
            return (await JsonSerializer.DeserializeAsync<List<TSource>>(stream)).ToList();
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
        using var stream = File.Create(path);
        await JsonSerializer.SerializeAsync(stream, data);
    }
}
