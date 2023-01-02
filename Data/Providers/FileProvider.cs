using BikeSparesInventorySystem.Data.Models;

namespace BikeSparesInventorySystem.Data.Providers;

internal abstract class FileProvider<TSource> where TSource : IModel
{
    internal abstract string FilePath { get; set; }

    internal abstract Task<ICollection<TSource>> ReadAsync(string path);
    internal abstract Task<ICollection<TSource>> ReadAsync(Stream stream);
    internal virtual async Task<ICollection<TSource>> ReadAsync()
    {
        return await ReadAsync(FilePath);
    }

    internal abstract Task WriteAsync(string path, ICollection<TSource> data);
    internal virtual async Task WriteAsync(ICollection<TSource> data)
    {
        await WriteAsync(FilePath, data);
    }
}
