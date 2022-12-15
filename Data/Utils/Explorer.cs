using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeSparesInventorySystem.Data.Utils
{
    internal static class Explorer
    {
        public static string GetAppDataDirectoryPath()
        {
            var path = Path.Combine(
                Environment.CurrentDirectory,
                "AppData"
            );
            System.Diagnostics.Debug.WriteLine( "---------" );
            System.Diagnostics.Debug.WriteLine( path );
            System.Diagnostics.Debug.WriteLine( "---------" );
            return @"D:\";
        }

        private static string ApppendExtension<TSource>(string extension) => Path.Combine(GetAppDataDirectoryPath(), $"{typeof(TSource).Name}{extension}");

        public static string GetCsvFilePath<TSource>() => ApppendExtension<TSource>(".csv");

        public static string GetExcelFilePath<TSource>() => ApppendExtension<TSource>(".xlsx");

        public static string GetJsonFilePath<TSource>() => ApppendExtension<TSource>(".json");
    }
}
