using BikeSparesInventorySystem.Data.Models;
using BikeSparesInventorySystem.Data.Services;
using Microsoft.AspNetCore.Components;

namespace BikeSparesInventorySystem.Pages
{
    public partial class Login
    {
        
        private string _username { get; set; }
        private string _password { get; set; }
        private bool _isInitialUserCreated { get; set; } = false;

        private string _errorMessage;

        protected override void OnInitialized()
        {
            string username = _authService.SeedInitialUser();
            if (username is not null)
            {
                _isInitialUserCreated= true;
                _username = username;
                _password = username;
            }
        }

        private void LoginHandler()
        {
            try
            {
                _errorMessage = null;
                if (_authService.Login(_username, _password))
                {
                    _navigationManager.NavigateTo(_authService.CurrentUser.HasInitialPassword ? "/change-password" : "/");
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
        }
    }
}