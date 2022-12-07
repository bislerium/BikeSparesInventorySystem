using BikeSparesInventorySystem.Data.Models;

namespace BikeSparesInventorySystem.Data.Providers
{
    internal interface IFileProvider<TSource> where TSource : IModel
    {
        string FilePath { get; set; }

        List<TSource> Read();

        bool Write(TSource data);
    }
}
