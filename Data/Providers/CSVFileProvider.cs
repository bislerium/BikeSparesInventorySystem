using BikeSparesInventorySystem.Data.Models;
using BikeSparesInventorySystem.Data.Utils;
using CsvHelper;
using CsvHelper.Configuration;
using NPOI.HSSF.Record;
using System.Globalization;

namespace BikeSparesInventorySystem.Data.Providers
{
    internal class CsvFileProvider<TSource> : FileProvider<TSource> where TSource : IModel
    {
        internal override string FilePath { get; set; } = Explorer.GetCsvFilePath<TSource>();

        internal override async Task<ICollection<TSource>> ReadAsync(string path)
        {
            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            List<TSource> data = new();
            await foreach(TSource r in csv.GetRecordsAsync<TSource>()) data.Add(r);
            data.ForEach(r => System.Diagnostics.Debug.WriteLine(r));
            return data;
        }

        internal override async Task WriteAsync(string path, ICollection<TSource> data)
        {
            using var writer = new StreamWriter(path);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            await csv.WriteRecordsAsync(data);
        }
    }
}
