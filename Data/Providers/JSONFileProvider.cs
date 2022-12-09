using BikeSparesInventorySystem.Data.Models;
using NPOI.Util;
using System.Text.Json;

namespace BikeSparesInventorySystem.Data.Providers
{
    internal class JSONFileProvider<TSource> : IFileProvider<TSource> where TSource : IModel
    {
        public string FilePath { get; set; }

        public ICollection<TSource> Read()
        {
            using var stream = File.OpenRead(FilePath);
            return JsonSerializer.Deserialize<IEnumerable<TSource>>(stream).ToList();
        }

        public void Write(TSource data)
        {
            using (var fs = new FileStream(FilePath, FileMode.OpenOrCreate))
            {
                
                byte[] content = JsonSerializer.SerializeToUtf8Bytes(data);
                fs.Seek(-1, SeekOrigin.End);
                fs.Write(content, 0, content.Length); // include a leading comma character if required
                //fs.Write(squareBracketByte, 0, 1);
                //fs.SetLength(fs.Position); //Only needed if new content may be smaller than old
            }
        }

        public void Write(ICollection<TSource> data)
        {
            using var stream = File.Create(FilePath);
            JsonSerializer.Serialize(stream, data);
        }
    }
}
