using BikeSparesInventorySystem.Data.Models;
using Ganss.Excel;

namespace BikeSparesInventorySystem.Data.Providers
{
    internal class ExcelFileProvider<TSource> : FileProvider<TSource> where TSource : IModel
    {
        internal override async Task<ICollection<TSource>> ReadAsync(string path) => (await new ExcelMapper().FetchAsync<TSource>(path)).ToList();

        internal override async Task WriteAsync(string path, ICollection<TSource> data) => await new ExcelMapper().SaveAsync(path, data, nameof(TSource));
    }        
}
