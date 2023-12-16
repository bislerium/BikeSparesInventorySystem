namespace BikeSparesInventorySystem.Components.Pages;

public partial class ChangePassword
{
    public const string Route = "/change-password";

    private MudForm _form;
    private string _currentPassword;
    private string _newPassword;

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
            _form.Validate();
            if (_form.IsValid)
            {
                _authService.ChangePassword(_currentPassword, _newPassword);
                SnackBar.Add("The password has been changed successfully.", Severity.Success);
                _form.ResetAsync();
            }
        }
        catch (Exception e)
        {
            SnackBar.Add(e.Message, Severity.Error);
        }
    }
}
