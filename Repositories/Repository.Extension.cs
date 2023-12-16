namespace BikeSparesInventorySystem.Repositories;

internal static class Repository
{
    public static bool HasUserName(this Repository<User> userRepository, string userName)
    {
        return userRepository.Contains(x => x.UserName, userName);
    }

    public static void OnDebugConsoleWriteUserNames(this Repository<User> userRepository)
    {
        foreach (User i in userRepository.GetAll())
        {
            System.Diagnostics.Debug.WriteLine($"{{ Username = {i.UserName} , InitialPassword = {i.HasInitialPassword} , Role = {i.Role}}}");
        }
    }
}
