using BikeSparesInventorySystem.Data.Models;

namespace BikeSparesInventorySystem.Data.Repositories
{
    internal static class Repository
    {
        public static bool HasUserName(this Repository<User> userRepository, string userName) => userRepository.Contains(x => x.Username, userName);

    }
}
