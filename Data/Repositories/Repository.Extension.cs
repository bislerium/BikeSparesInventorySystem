using BikeSparesInventorySystem.Data.Models;

namespace BikeSparesInventorySystem.Data.Repositories
{
    internal static class Repository
    {
        public static bool HasUserName(this Repository<User> userRepository, string userName)
        {
            return userRepository.Contains(x => x.UserName, userName);
        }
    }
}
