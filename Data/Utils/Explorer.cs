using BikeSparesInventorySystem.Data.Models;
using BikeSparesInventorySystem.Data.Providers;

namespace BikeSparesInventorySystem.Data.Utils;

internal static class Explorer
{
    // 10MB
    public const int MAX_ALLOWED_IMPORT_SIZE = 1024 * 1024 * 10;

    public static string GetAppDataDirectoryPath()
    {
        var path = Path.Combine(
            Environment.CurrentDirectory,
            "AppData"
        );
        return @"D:\";
    }

    public static FileProvider<TSource> GetFileProvider<TSource>(string fileName) where TSource : IModel
    {
        string extension = fileName.Split(".").Last();
        return extension.ToLower() switch
        {
            "csv" => new CsvFileProvider<TSource>(),
            "json" => new JsonFileProvider<TSource>(),
            "xlsx" => new ExcelFileProvider<TSource>(),
            _ => throw new Exception("Supports JSON, CSV or Excel(.xlsx) File Only !")
        };
    }

    private static string ApppendExtension<TSource>(string extension) => Path.Combine(GetAppDataDirectoryPath(), $"{typeof(TSource).Name}{extension}");

    public static string GetCsvFilePath<TSource>() => ApppendExtension<TSource>(".csv");

    public static string GetExcelFilePath<TSource>() => ApppendExtension<TSource>(".xlsx");

    public static string GetJsonFilePath<TSource>() => ApppendExtension<TSource>(".json");
}
