using BikeSparesInventorySystem.Data.Models;

namespace BikeSparesInventorySystem.Data.Providers
{
    internal class JSONFileProvider<TSource> : IFileProvider<TSource> where TSource : IModel
    {
        public string FilePath { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public List<TSource> Read()
        {
            throw new NotImplementedException();
        }

        public bool Write(TSource data)
        {
            throw new NotImplementedException();
        }
    }
}
