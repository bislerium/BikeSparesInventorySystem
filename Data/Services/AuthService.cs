using BikeSparesInventorySystem.Data.Enums;
using BikeSparesInventorySystem.Data.Models;
using BikeSparesInventorySystem.Data.Repositories;
using BikeSparesInventorySystem.Data.Utils;

namespace BikeSparesInventorySystem.Data.Services
{
    internal class AuthService
    {
        private readonly Repository<User> _userRepository;
        private User _user;
        public User User { get => _user; }

        public AuthService( Repository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public void Register(string username, UserRole userRole, string password)
        {
            if (_userRepository.HasUserName(username)) throw new Exception(message: "Username already exists!");
            User user = new() { 
                Username = username,
                PasswordHash = Hasher.HashSecret(password),
                Role = userRole,
                CreatedAt= DateTime.UtcNow,
                CreatedBy = _user.Id,
            };
            _userRepository.Add(user);
        }

        public bool Login(string userName, string password)
        {
            _user = _userRepository.Get(x => x.Username, userName);
            if (_user == null) return false;
            return Hasher.VerifyHash(password, User.PasswordHash);
        }
    }
}
