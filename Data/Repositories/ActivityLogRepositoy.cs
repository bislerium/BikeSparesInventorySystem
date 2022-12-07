using BikeSparesInventorySystem.Data.Enums;
using BikeSparesInventorySystem.Data.Models;

namespace BikeSparesInventorySystem.Data.Repositories
{
    internal class InventoryLogRepository : IRepository<ActivityLog>
    {
        public int Count => throw new NotImplementedException();

        public void Add(ActivityLog item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(ActivityLog item)
        {
            throw new NotImplementedException();
        }

        public ActivityLog Get<TKey>(Func<ActivityLog, TKey> keySelector, TKey byValue)
        {
            throw new NotImplementedException();
        }

        public List<ActivityLog> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<ActivityLog> GetAllSorted<TKey>(Func<ActivityLog, TKey> keySelector, SortDirection direction)
        {
            throw new NotImplementedException();
        }

        public bool Remove(ActivityLog item)
        {
            throw new NotImplementedException();
        }

        public void Update(ActivityLog item)
        {
            throw new NotImplementedException();
        }
    }
}
