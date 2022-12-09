using BikeSparesInventorySystem.Data.Models;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace BikeSparesInventorySystem.Data.Providers
{
    internal class CSVFileProvider<TSource> : IFileProvider<TSource> where TSource : IModel
    {
        public string FilePath { get; set; }

        public ICollection<TSource> Read()
        {
            using var reader = new StreamReader(FilePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csv.GetRecords<TSource>().ToList();
        }

        public static void Write(string filePath, TSource data, bool append = true)
        {
            using var csvWriter = GetCsvWriter(filePath, append);
            Write(csvWriter, data);
        }

        public static void Write(string filePath, ICollection<TSource> data, bool append = false)
        {
            using var csvWriter = GetCsvWriter(filePath, append);
            Write(csvWriter, data);
        }

        public void Write(TSource data) => CSVFileProvider<TSource>.Write(FilePath, data);

        public void Write(ICollection<TSource> data) => CSVFileProvider<TSource>.Write(FilePath, data);

        public static void Write(CsvWriter csvWriter, TSource data) => csvWriter.WriteRecord(data);

        public static void Write(CsvWriter csvWriter, ICollection<TSource> data) => csvWriter.WriteRecord(data);

        public static CsvWriter GetCsvWriter(string filePath, bool append = false)
        {            
            using var stream = File.Open(filePath, append ? FileMode.Append : FileMode.Create);
            using var writer = new StreamWriter(stream);
            return append
                ? new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    // Don't write the header again.
                    HasHeaderRecord = false,
                })
                : new CsvWriter(writer, CultureInfo.InvariantCulture);
        }
    }
}
