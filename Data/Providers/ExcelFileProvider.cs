using BikeSparesInventorySystem.Data.Models;
using Ganss.Excel;

namespace BikeSparesInventorySystem.Data.Providers
{
    internal class ExcelFileProvider<TSource> : IFileProvider<TSource> where TSource : IModel
    {
        public string FilePath { get; set; }

        private ExcelMapper _excelMapper;
        private ICollection<TSource> _sourceData;

        public ICollection<TSource> Read()
        {
            using var stream = File.OpenRead(FilePath);
            return _sourceData = new ExcelMapper(stream).Fetch<TSource>().ToList();
        }

        public void Write(TSource data)
        {
            _excelMapper = new ExcelMapper("products.xlsx");
            _ = _sourceData.Append(data);
            _excelMapper.Save(FilePath);
        }

        public void Write(ICollection<TSource> data)
        {
            var stream = File.OpenWrite(FilePath);
            new ExcelMapper().Save(stream, data, nameof(TSource));
        }
    }
}
