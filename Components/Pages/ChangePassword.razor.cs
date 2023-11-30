namespace BikeSparesInventorySystem.Components.Pages;

public partial class ChangePassword
{
    public const string Route = "/change-password";

    private MudForm Form;
    private string CurrentPassword { get; set; }
    private string NewPassword { get; set; }

    [CascadingParameter]
    private Action<string> SetAppBarTitle { get; set; }

    protected sealed override void OnInitialized()
    {
        SetAppBarTitle.Invoke("Change Password");
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
                Form.ResetAsync();
            }
        }
        catch (Exception e)
        {
            SnackBar.Add(e.Message, Severity.Error);
        }
    }
}
