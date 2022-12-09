using BikeSparesInventorySystem.Data.Enums;
using BikeSparesInventorySystem.Data.Models;
using System.Linq;

namespace BikeSparesInventorySystem.Data.Repositories
{
    internal class Repository<TSource> : IRepository<TSource> where TSource : IModel
    {
        readonly ICollection<TSource> _sourceData;

        public int Count => _sourceData.Count;

        public void Add(TSource item) => _sourceData.Add(item);

        public void Clear() => _sourceData.Clear();

        public bool Contains(TSource item) => _sourceData.Contains(item);

        public bool Contains<TKey>(Func<TSource, TKey> keySelector, TKey byValue) => Get(keySelector, byValue) is not null;

        public TSource Get<TKey>(Func<TSource, TKey> keySelector, TKey byValue) => _sourceData.FirstOrDefault(a => keySelector.Invoke(a).Equals(byValue));

        public ICollection<TSource> GetAll() => _sourceData;

        public ICollection<TSource> GetAllSorted<TKey>(Func<TSource, TKey> keySelector, SortDirection direction) => direction switch
        {
            SortDirection.Ascending => _sourceData.OrderBy(keySelector).ToList(),
            SortDirection.Descending => _sourceData.OrderByDescending(keySelector).ToList(),
            _ => throw new Exception("Invalid sort direction!"),
        };

        public bool Remove(TSource item) => _sourceData.Remove(item);

        public void Update(TSource item) => _sourceData.Add(item);
    }
}
