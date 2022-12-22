using BikeSparesInventorySystem.Data.Utils;
using MudBlazor;

namespace BikeSparesInventorySystem.Pages
{
    public partial class Login
    {

        private MudForm form;
        private string Username { get; set; }
        private string Password { get; set; }

        protected override void OnInitialized()
        {
            string username = _authService.SeedInitialUser();
            if (username is not null)
            {
                SnackBar.Add("Initial user created!", Severity.Info);
                Username = username;
                Password = username;
            }
            Seeder.OnDebugConsoleWriteUserNames(UserRepository.GetAll());
        }

        private void LoginHandler()
        {
            string _errorMessage;
            try
            {
                _errorMessage = null;

                form.Validate();

                if (form.IsValid)
                {
                    if (_authService.Login(Username, Password))
                    {
                        if (_authService.CurrentUser.HasInitialPassword)
                        {
                            SnackBar.Add("Please change your password!", Severity.Warning);
                            _navigationManager.NavigateTo("/change-password");
                        }
                        else
                        {
                            _navigationManager.NavigateTo("/");
                        }
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


    }
}