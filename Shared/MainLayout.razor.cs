using MudBlazor;

namespace BikeSparesInventorySystem.Shared;

public partial class MainLayout
{
    public static string Title { get; set; }

    private bool _drawerOpen = true;

    void DrawerToggle()
    {
        _drawerOpen = !_drawerOpen;
    }

    async Task Logout() 
    {
        var parameters = new DialogParameters
        {
            { "ContentText", "Do you really want to Logout?" },
            { "ButtonText", "Logout" },
            { "Color", MudBlazor.Color.Error }
        };

        var options = new DialogOptions() { CloseOnEscapeKey = true };

        var dialog = await DialogService.ShowAsync<Dialog>("Logout", parameters, options);
        var result = await dialog.Result;

        if (!result.Cancelled)
        {
            // AuthService.LogOut();
            Snackbar.Clear();
            Snackbar.Add("Logged out!", Severity.Success);
            NavigationManager.NavigateTo("/login", replace: true);
        }            
    }
}
