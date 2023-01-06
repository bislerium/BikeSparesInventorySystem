namespace BikeSparesInventorySystem.Shared.Layouts;

public partial class MainLayout
{
    private string _title;

    private bool _drawerOpen = true;

    private void DrawerToggle()
    {
        _drawerOpen = !_drawerOpen;
    }

    private void SetAppBarTitle(string title)
    {
        _title = title;
    }
}
