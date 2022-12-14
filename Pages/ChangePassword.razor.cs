using BikeSparesInventorySystem.Data.Services;
using Microsoft.AspNetCore.Components;

namespace BikeSparesInventorySystem.Pages
{
    public partial class ChangePassword
    {
        private readonly AuthService _authService;
        private readonly NavigationManager _navigationManager;

        internal ChangePassword(AuthService authService, NavigationManager navigationManager)
        {
            _authService = authService;
            _navigationManager = navigationManager;
        }

        private string _currentPassword { get; set; }
        private string _newPassword { get; set; }
        private string _errorMessage;
        private string _successMessage;

        private void ChangePasswordHandler()
        {
            try
            {
                _authService.ChangePassword(_currentPassword, _newPassword);                
                _successMessage = "The password has been changed successfully.";
                _navigationManager.NavigateTo("/");
            }
            catch (Exception e)
            {
                _errorMessage = e.Message;
                Console.WriteLine(e);
            }
        }
    }

}
}