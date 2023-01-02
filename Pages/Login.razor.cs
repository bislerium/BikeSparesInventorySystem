using BikeSparesInventorySystem.Data.Utils;
using MudBlazor;

namespace BikeSparesInventorySystem.Pages
{
    public partial class Login
    {

        private MudForm Form;
        private string Username { get; set; }
        private string Password { get; set; }

        protected sealed override void OnInitialized()
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

        private async Task LoginHandler()
        {
            string _errorMessage;
            try
            {
                _errorMessage = null;

                await Form.Validate();

                if (Form.IsValid)
                {
                    if (await _authService.Login(Username, Password))
                    {
                        _navigationManager.NavigateTo("/router");
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