namespace BikeSparesInventorySystem.Components.Pages;

public partial class Login
{
    public const string Route = "/login";

    private MudForm _form;
    private string _username;
    private string _password;
    private bool _stayLoggedIn;

    protected sealed override async Task OnInitializedAsync()
    {
        string? username = await _authService.SeedInitialUser();
        if (username is not null)
        {
            _username = username;
            _password = username;

            SnackBar.Add("Initial user created!", Severity.Info);
        }
        Seeder.OnDebugConsoleWriteUserNames(UserRepository.GetAll());
    }

    private void SetSeedUser(string username)
    {
        _username = username;
        _password = username;
        StateHasChanged();
    }

    private async Task LoginHandler()
    {
        string? _errorMessage = null;
        try
        {
            await _form.Validate();

            if (_form.IsValid)
            {
                if (await _authService.Login(_username, _password, _stayLoggedIn))
                {
                    _navigationManager.NavigateTo(RoleRouter.Route);
                    return;
                }
                else
                {
                    _errorMessage = "Incorrect username or password!";
                }
            }
        }
        catch (Exception e)
        {
            _errorMessage = e.Message;
        }
        SnackBar.Add(_errorMessage, Severity.Error);
    }

    private bool _isShow;
    private InputType _passwordInput = InputType.Password;
    private string _passwordInputIcon = Icons.Material.Filled.VisibilityOff;

    private void ButtonTestclick()
    {
        if (_isShow)
        {
            _isShow = false;
            _passwordInputIcon = Icons.Material.Filled.VisibilityOff;
            _passwordInput = InputType.Password;
        }
        else
        {
            _isShow = true;
            _passwordInputIcon = Icons.Material.Filled.Visibility;
            _passwordInput = InputType.Text;
        }
    }
}