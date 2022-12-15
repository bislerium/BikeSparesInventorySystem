using BikeSparesInventorySystem.Data.Models;

namespace BikeSparesInventorySystem.Data.Providers
{
    internal abstract class FileProvider<TSource> where TSource : IModel
    {
        // internal virtual string FilePath { get; set; } = FileSystem.Current.AppDataDirectory;
        internal abstract string FilePath { get; set; }
        internal abstract Task<ICollection<TSource>> ReadAsync(string path);
        internal virtual async Task<ICollection<TSource>> ReadAsync() => await ReadAsync(FilePath);
        internal abstract Task WriteAsync(string path, ICollection<TSource> data) ;
        internal virtual async Task WriteAsync(ICollection<TSource> data) => await WriteAsync(FilePath, data);        
    }
}
