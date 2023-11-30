namespace BikeSparesInventorySystem.Components.Pages;

public partial class RoleRouter
{
    public const string Route = "/router";

    protected sealed override void OnInitialized()
    {
        if (AuthService.CurrentUser.HasInitialPassword)
        {
            Snackbar.Add("Please change your password!", Severity.Warning);
            NavigationManager.NavigateTo("/change-password");
        }
        else
        {
            if (AuthService.IsUserAdmin())
            {
                NavigationManager.NavigateTo(Dashboard.Route);
            }
            else
            {
                NavigationManager.NavigateTo(Inventory.Route);
            }
        }
    }
}
