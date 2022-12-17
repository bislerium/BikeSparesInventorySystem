using BikeSparesInventorySystem.Data.Enums;
using BikeSparesInventorySystem.Data.Models;
using BikeSparesInventorySystem.Data.Repositories;
using BikeSparesInventorySystem.Data.Utils;

namespace BikeSparesInventorySystem.Data.Services;

internal class AuthService
{
    private readonly Repository<User> _userRepository;
    private User _user;
    public User CurrentUser { get => _user; }

    public AuthService(Repository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public string SeedInitialUser()
    {
        if (_userRepository.GetAll().Count != 0) return null;
        if (_userRepository.Contains(x => x.Role, UserRole.Admin)) return null;
        string username = "admin", pleaseChange = "Please Change!";
        User user = new ()
        {
            UserName = username,
            Email = pleaseChange,
            FullName = pleaseChange,
            PasswordHash = Hasher.HashSecret(username),
            Role = UserRole.Admin,
            CreatedBy = Guid.Empty,
        };
        _userRepository.Add(user);
        _userRepository.FlushAsync().Wait();
        return username;
    }

    public void Register(string username, UserRole userRole, string password)
    {
        if (_userRepository.HasUserName(username)) throw new Exception(message: "Username already exists!");
        User user = new() { 
            UserName = username,
            PasswordHash = Hasher.HashSecret(password),
            Role = userRole,
            CreatedAt= DateTime.UtcNow,
            CreatedBy = _user.Id,
        };
        _userRepository.Add(user);
        _userRepository.FlushAsync().Wait();
    }

    public bool Login(string userName, string password)
    {
        _user = _userRepository.Get(x => x.UserName, userName);
        if (_user == null) return false;
        return Hasher.VerifyHash(password, CurrentUser.PasswordHash);
    }

    public void ChangePassword(string oldPassword, string newPassword)
    {
        if (oldPassword == newPassword)
        {
            throw new Exception("New password must be different from current password.");
        }

        _user.PasswordHash = Hasher.HashSecret(newPassword);
        _user.HasInitialPassword = false;
        _userRepository.FlushAsync().Wait();
    }

    public void LogOut() => _user = null;
}
