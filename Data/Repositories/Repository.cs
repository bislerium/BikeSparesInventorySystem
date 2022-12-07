using BikeSparesInventorySystem.Data.Enums;
using BikeSparesInventorySystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeSparesInventorySystem.Data.Repositories
{
    internal class Repository<TSource> : IRepository<TSource> where TSource : IModel
    {
        readonly List<TSource> sourceData;

        public int Count => sourceData.Count;

        public void Add(TSource item) => sourceData.Add(item);

        public void Clear() => sourceData.Clear();

        public bool Contains(TSource item) => sourceData.Contains(item);

        public TSource Get<TKey>(Func<TSource, TKey> keySelector, TKey byValue) => sourceData.FirstOrDefault(a => keySelector.Invoke(a).Equals(byValue));

        public IEnumerable<TSource> GetAll() => sourceData;

        public IEnumerable<TSource> GetAllSorted<TKey>(Func<TSource, TKey> keySelector, SortDirection direction) => direction switch
        {
            SortDirection.Ascending => sourceData.OrderBy(keySelector).ToList(),
            SortDirection.Descending => sourceData.OrderByDescending(keySelector).ToList(),
            _ => Enumerable.Empty<TSource>(),
        };

        public bool Remove(TSource item) => sourceData.Remove(item);

        public void Update(TSource item) => 
    }
}
