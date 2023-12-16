namespace BikeSparesInventorySystem.Utils;

internal static class Explorer
{
    // 10MB
    public const int MAX_ALLOWED_IMPORT_SIZE = 1024 * 1024 * 10;

    public static string GetAppDataDirectoryPath()
    {
        return FileSystem.AppDataDirectory;
    }

    public static FileProvider<TSource> GetFileProvider<TSource>(FileExtension extension) where TSource : IModel
    {
        return extension switch
        {
            FileExtension.csv => new CsvFileProvider<TSource>(),
            FileExtension.json => new JsonFileProvider<TSource>(),
            FileExtension.xlsx => new ExcelFileProvider<TSource>(),
            _ => throw new Exception("Supports JSON, CSV or Excel(.xlsx) File Only!")
        };
    }

    public static string GetDesktopDirectoryPath()
    {
        return Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
    }

    public static string GetFilePath(string Directory, string fileName)
    {
        return Path.Combine(Directory, fileName);
    }

    public static string GetFile<TSource>(FileExtension extension)
    {
        return GetFile(typeof(TSource).Name, extension);
    }

    public static string GetFile(string fileName, FileExtension extension)
    {
        return $"{fileName}.{Enum.GetName(extension)}";
    }

    public static string GetDefaultExportFilePath<TSource>(FileExtension extension)
    {
        return GetFilePath(GetDesktopDirectoryPath(), GetFile<TSource>(extension));
    }

    public static string GetDefaultFilePath<TSource>(FileExtension extension)
    {
        return GetFilePath(GetAppDataDirectoryPath(), GetFile<TSource>(extension));
    }
}
