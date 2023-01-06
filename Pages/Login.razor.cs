namespace BikeSparesInventorySystem.Pages;

public partial class Login
{
    public const string Route = "/login";

    private MudForm Form;
    private string Username { get; set; }
    private string Password { get; set; }
    private bool StayLoggedIn { get; set; }

    protected sealed override async Task OnInitializedAsync()
    {
        string username = await _authService.SeedInitialUser();
        if (username is not null)
        {
            Username = username;
            Password = username;

            SnackBar.Add("Initial user created!", Severity.Info);
        }
        Seeder.OnDebugConsoleWriteUserNames(UserRepository.GetAll());
    }

    private void SetSeedUser(string username)
    {
        Username = username;
        Password = username;
        StateHasChanged();
    }

    private async Task LoginHandler()
    {
        string _errorMessage;
        try
        {
            _errorMessage = null;

            await Form.Validate();

            if (Form.IsValid)
            {
                if (await _authService.Login(Username, Password, StayLoggedIn))
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

    private bool isShow;
    private InputType PasswordInput = InputType.Password;
    private string PasswordInputIcon = Icons.Material.Filled.VisibilityOff;

    private void ButtonTestclick()
    {
        if (isShow)
        {
            isShow = false;
            PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
            PasswordInput = InputType.Password;
        }
        else
        {
            isShow = true;
            PasswordInputIcon = Icons.Material.Filled.Visibility;
            PasswordInput = InputType.Text;
        }
    }
}