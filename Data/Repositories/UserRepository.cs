using BikeSparesInventorySystem.Data.Enums;
using BikeSparesInventorySystem.Data.Models;
using System.Security.Cryptography.X509Certificates;

namespace BikeSparesInventorySystem.Data.Repositories
{
    internal class UserRepository : IRepository<User>
    {
        public int Count => throw new NotImplementedException();

        public void Add(User item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(User item)
        {
            throw new NotImplementedException();
        }

        public User Get<TKey>(Func<User, TKey> keySelector, TKey byValue)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<User> GetAllSorted<TKey>(Func<User, TKey> keySelector, SortDirection direction)
        {
            throw new NotImplementedException();
        }

        public bool Remove(User item)
        {
            throw new NotImplementedException();
        }

        public void Update(User item)
        {
            throw new NotImplementedException();
        }
    }
}
