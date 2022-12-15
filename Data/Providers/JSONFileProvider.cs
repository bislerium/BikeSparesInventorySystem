using BikeSparesInventorySystem.Data.Models;
using BikeSparesInventorySystem.Data.Utils;
using System.Text.Json;

namespace BikeSparesInventorySystem.Data.Providers
{
    internal class JsonFileProvider<TSource> : FileProvider<TSource> where TSource : IModel
    {
        internal override string FilePath { get; set; } = Explorer.GetJsonFilePath<TSource>();

        internal override async Task<ICollection<TSource>> ReadAsync(string path)
        {
            using var stream = File.OpenRead(path);
            return (await JsonSerializer.DeserializeAsync<List<TSource>>(stream)).ToList();
        }

        internal override async Task WriteAsync(string path, ICollection<TSource> data)
        {
            using var stream = File.Create(path);
            await JsonSerializer.SerializeAsync(stream, data);
        }
    }
}
