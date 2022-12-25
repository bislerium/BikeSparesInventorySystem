using BikeSparesInventorySystem.Shared.Layouts;
using MudBlazor;

namespace BikeSparesInventorySystem.Pages
{
    public partial class ChangePassword
    {
        private MudForm Form;
        private string CurrentPassword { get; set; }
        private string NewPassword { get; set; }

        protected sealed override void OnInitialized()
        {
            MainLayout.Title = "Change Password";
        }

        private void ChangePasswordHandler()
        {
            try
            {
                Form.Validate();
                if (Form.IsValid)
                {
                    _authService.ChangePassword(CurrentPassword, NewPassword);
                    SnackBar.Add("The password has been changed successfully.", Severity.Success);
                    Form.Reset();
                }
            }
            catch (Exception e)
            {
                SnackBar.Add(e.Message, Severity.Error);
            }
        }
    }
}
