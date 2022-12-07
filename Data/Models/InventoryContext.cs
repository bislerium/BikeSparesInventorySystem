using BikeSparesInventorySystem.Data.Repositories;

namespace BikeSparesInventorySystem.Data.Models
{
    internal class InventoryContext
    {
        internal Repository<ActivityLog> ActivityLogs { get; }
        internal Repository<Spare> Spares { get; }
        internal Repository<User> Users { get;  }

        public InventoryContext(Repository<ActivityLog> activityLogs, Repository<Spare> spares, Repository<User> users)
        {
            ActivityLogs = activityLogs;
            Spares = spares;
            Users = users;
        }
    }
}
