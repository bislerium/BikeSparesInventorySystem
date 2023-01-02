using BikeSparesInventorySystem.Data.Models;
using BikeSparesInventorySystem.Data.Utils;
using CsvHelper;
using System.Globalization;

namespace BikeSparesInventorySystem.Data.Providers;

internal class CsvFileProvider<TSource> : FileProvider<TSource> where TSource : IModel
{
    internal override string FilePath { get; set; } = Explorer.GetDefaultFilePath<TSource>(Enums.FileExtension.csv);

    internal override async Task<ICollection<TSource>> ReadAsync(string path)
    {
        return await ReadAsync(File.OpenRead(path));
    }

    internal override async Task<ICollection<TSource>> ReadAsync(Stream stream)
    {
        try
        {
            using StreamReader reader = new(stream);
            using CsvReader csv = new(reader, CultureInfo.InvariantCulture);
            List<TSource> data = new();
            await foreach (TSource r in csv.GetRecordsAsync<TSource>()) data.Add(r);
            return data;
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
        using StreamWriter writer = new(path);
        using CsvWriter csv = new(writer, CultureInfo.InvariantCulture);
        await csv.WriteRecordsAsync(data);
    }
}
