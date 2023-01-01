using BikeSparesInventorySystem.Data.Enums;
using BikeSparesInventorySystem.Data.Models;
using BikeSparesInventorySystem.Data.Repositories;
using BikeSparesInventorySystem.Data.Utils;

namespace BikeSparesInventorySystem.Data.Services;

internal class AuthService
{
	private readonly Repository<User> _userRepository;

	private readonly SessionService _sessionService;

	private User _user;
	public User CurrentUser { get => _user; }

	public AuthService(Repository<User> userRepository, SessionService sessionService)
	{
		_userRepository = userRepository;
		_sessionService = sessionService;

	}

	public string SeedInitialUser()
	{
		if (_userRepository.GetAll().Count != 0) return null;
		if (_userRepository.Contains(x => x.Role, UserRole.Admin)) return null;
		string username = "admin", pleaseChange = "Please Change!";
		User user = new()
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

	public void Register(string username, string email, string fullname, UserRole role)
	{
		if (_userRepository.HasUserName(username)) throw new Exception(message: "Username already exists!");
		User user = new()
		{
			UserName = username,
			Email = email,
			FullName = fullname,
			PasswordHash = Hasher.HashSecret(username),
			Role = role,
			CreatedBy = _user.Id,
		};
		_userRepository.Add(user);
		//_userRepository.FlushAsync().Wait();
	}

	public async Task<bool> Login(string userName, string password)
	{
		_user = _userRepository.Get(x => x.UserName, userName);
		if (_user == null) return false;
		if (Hasher.VerifyHash(password, CurrentUser.PasswordHash))
		{
			Session session = new()
			{
				UserId = _user.Id,
				CreatedAt = DateTime.UtcNow,
			};
			await _sessionService.SaveSession(session);
			return true;
		}
		return false;
	}

	public void ChangePassword(string oldPassword, string newPassword)
	{
		if (oldPassword == newPassword)
		{
			throw new Exception("New password must be different from current password.");
		}

		_user.PasswordHash = Hasher.HashSecret(newPassword);
		_user.HasInitialPassword = false;
	}

	public void LogOut()
	{
		_sessionService.DeleteSession();
		_user = null;
	}

	public async Task CheckSession()
	{
		Session session = await _sessionService.LoadSession();
		if (session == null) return;

		User user = _userRepository.Get(x => x.Id, session.UserId);
		if (user == null) return;

		if (!session.IsValid()) throw new Exception("Session expired!");
		_user = user;
	}
}
