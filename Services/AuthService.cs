namespace BikeSparesInventorySystem.Services;

internal class AuthService
{
    private readonly Repository<User> _userRepository;

    private readonly SessionService _sessionService;

    public User? CurrentUser { get; private set; }

    public AuthService(Repository<User> userRepository, SessionService sessionService)
    {
        _userRepository = userRepository;
        _sessionService = sessionService;
    }

    public async Task<string?> SeedInitialUser()
    {
        if (_userRepository.GetAll().Count != 0)
        {
            return null;
        }

        if (_userRepository.Contains(x => x.Role, UserRole.Admin))
        {
            return null;
        }

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
        await _userRepository.FlushAsync();
        return username;
    }

    public void Register(string username, string email, string fullname, UserRole role)
    {
        if (_userRepository.HasUserName(username))
        {
            throw new Exception(message: "Username already exists!");
        }

        User user = new()
        {
            UserName = username,
            Email = email,
            FullName = fullname,
            PasswordHash = Hasher.HashSecret(username),
            Role = role,
            CreatedBy = CurrentUser.Id,
        };
        _userRepository.Add(user);
    }

    public async Task<bool> Login(string userName, string password, bool stayLoggedIn)
    {
        CurrentUser = _userRepository.Get(x => x.UserName, userName);

        if (CurrentUser is null)
        {
            return false;
        }

        if (Hasher.VerifyHash(password, CurrentUser.PasswordHash))
        {
            Session session = Session.Generate(CurrentUser.Id, stayLoggedIn);
            await _sessionService.SaveSession(session);
            return true;
        }
        return false;
    }

    public bool IsUserAdmin()
    {
        return CurrentUser.Role == UserRole.Admin;
    }

    public void ChangePassword(string oldPassword, string newPassword)
    {
        if (oldPassword == newPassword)
        {
            throw new Exception("New password must be different from current password.");
        }

        CurrentUser.PasswordHash = Hasher.HashSecret(newPassword);
        CurrentUser.HasInitialPassword = false;
    }

    public void LogOut()
    {
        _sessionService.DeleteSession();
        CurrentUser = null;
    }

    public async Task CheckSession()
    {
        Session? session = await _sessionService.LoadSession();
        if (session is null)
        {
            return;
        }

        User? user = _userRepository.Get(x => x.Id, session.UserId);
        if (user is null)
        {
            return;
        }

        if (!session.IsValid())
        {
            throw new Exception("Session expired!");
        }

        CurrentUser = user;
    }
}
