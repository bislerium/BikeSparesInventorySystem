using MudBlazor;

namespace BikeSparesInventorySystem.Pages
{
    public partial class Login
    {
        
        private string _username { get; set; }
        private string _password { get; set; }

        protected override void OnInitialized()
        {
            string username = _authService.SeedInitialUser();
            if (username is not null)
            {
                SnackBar.Add("Initial user created!", Severity.Info);
                _username = username;
                _password = username;
            }
            OnConsoleWriteUserNames();
        }

        private void OnConsoleWriteUserNames()
        {
            foreach (var i in UserRepository.GetAll())
            {
                System.Diagnostics.Debug.WriteLine($"{i.UserName} | {i.HasInitialPassword} | {i.Role}");
            }
        }

        private void LoginHandler()
        {
            string _errorMessage;
            try
            {
                _errorMessage = null;
                if (_authService.Login(_username, _password))
                {
                    if (_authService.CurrentUser.HasInitialPassword)
                    {
                        SnackBar.Add("Please Change the password!", Severity.Warning);
                        _navigationManager.NavigateTo("/change-password");
                    }
                    else
                    {
                        _navigationManager.NavigateTo("/");
                    }
                } else
                {
                    _errorMessage = "Incorrect username or password!";
                }
            }
            catch (Exception e)
            {
                _errorMessage = e.Message;
                Console.WriteLine(e);
            }
            SnackBar.Add(_errorMessage, Severity.Error);
        }

        private async Task SeedData()
        {
            await _seederService.SeedAsync();
            OnConsoleWriteUserNames();
            SnackBar.Add("Seeding Succesfull", Severity.Success);
        }
    }
}