namespace BikeSparesInventorySystem.Shared;

public partial class MainLayout
{
    public static string Title { get; set; }

    private bool _drawerOpen = true;

    void DrawerToggle()
    {
        _drawerOpen = !_drawerOpen;
    }
}
