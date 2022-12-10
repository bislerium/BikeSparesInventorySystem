using BikeSparesInventorySystem.Data.Repositories;

namespace BikeSparesInventorySystem.Data.Models
{
    internal class InventoryContext
    {
        internal IRepository<ActivityLog> ActivityLogs { get; }
        internal IRepository<Spare> Spares { get; }
        internal IRepository<User> Users { get;  }

        public InventoryContext(IRepository<ActivityLog> activityLogs, IRepository<Spare> spares, IRepository<User> users)
        {
            ActivityLogs = activityLogs;
            Spares = spares;
            Users = users;
        }
    }
}
