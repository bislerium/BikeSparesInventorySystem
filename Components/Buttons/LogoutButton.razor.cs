namespace BikeSparesInventorySystem.Components.Buttons;

public partial class LogoutButton
{
    private async Task Logout()
    {
        DialogParameters parameters = new()
        {
            { "ContentText", "Do you really want to Logout?" },
            { "ButtonText", "Logout" },
            { "Color", MudBlazor.Color.Error },
        };

        var dialog = await DialogService.ShowAsync<Dialog>("Logout", parameters);
        var result = await dialog.Result;

        if (!result!.Canceled)
        {
            AuthService.LogOut();
            Snackbar.Clear();
            Snackbar.Add("Logged out!", Severity.Success);
            NavigationManager.NavigateTo(Login.Route, replace: true);
        }
    }
}


