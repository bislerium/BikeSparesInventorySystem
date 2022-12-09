using BikeSparesInventorySystem.Data.Models;

namespace BikeSparesInventorySystem.Data.Providers
{
    internal interface IFileProvider<TSource> where TSource : IModel
    {
        string FilePath { get; set; }

        ICollection<TSource> Read();

        void Write(TSource data);

        void Write(ICollection<TSource> data);
    }
}
