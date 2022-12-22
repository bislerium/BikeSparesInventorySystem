using BikeSparesInventorySystem.Shared;
using MudBlazor;

namespace BikeSparesInventorySystem.Pages
{
    public partial class ChangePassword
    {
        private MudForm form;
        private string CurrentPassword { get; set; }
        private string NewPassword { get; set; }

        protected override void OnInitialized()
        {
            MainLayout.Title = "Change Password";
        }

        private void ChangePasswordHandler()
        {
            try
            {
                form.Validate();
                if (form.IsValid)
                {
                    _authService.ChangePassword(CurrentPassword, NewPassword);
                    SnackBar.Add("The password has been changed successfully.", Severity.Success);
                    form.Reset();
                }
            }
            catch (Exception e)
            {
                SnackBar.Add(e.Message, Severity.Error);
            }
        }
    }
}
