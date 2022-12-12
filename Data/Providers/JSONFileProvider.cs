using BikeSparesInventorySystem.Data.Models;
using System.Text.Json;

namespace BikeSparesInventorySystem.Data.Providers
{
    internal class JsonFileProvider<TSource> : FileProvider<TSource> where TSource : IModel
    {
        internal override async Task<ICollection<TSource>> ReadAsync(string path)
        {
            using var stream = File.OpenRead(path);
            return (await JsonSerializer.DeserializeAsync<IEnumerable<TSource>>(stream)).ToList();
        }

        internal override async Task WriteAsync(string path, ICollection<TSource> data)
        {
            using var stream = File.Create(path);
            await JsonSerializer.SerializeAsync(stream, data);
        }
    }
}
