namespace BikeSparesInventorySystem.Shared.Layouts;

public partial class MainLayout
{
    public static string Title { get; set; }

    private bool _drawerOpen = true;

    private void DrawerToggle()
    {
        _drawerOpen = !_drawerOpen;
    }
}
