using BikeSparesInventorySystem.Data.Enums;
using BikeSparesInventorySystem.Data.Models;

namespace BikeSparesInventorySystem.Data.Repositories
{
    internal class SpareRepository : IRepository<Spare>
    {
        public int Count => throw new NotImplementedException();

        public void Add(Spare item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(Spare item)
        {
            throw new NotImplementedException();
        }

        public Spare Get<TKey>(Func<Spare, TKey> keySelector, TKey byValue)
        {
            throw new NotImplementedException();
        }

        public List<Spare> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Spare> GetAllSorted<TKey>(Func<Spare, TKey> keySelector, SortDirection direction)
        {
            throw new NotImplementedException();
        }

        public bool Remove(Spare item)
        {
            throw new NotImplementedException();
        }

        public void Update(Spare item)
        {
            throw new NotImplementedException();
        }
    }
}
