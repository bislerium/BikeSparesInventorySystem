namespace BikeSparesInventorySystem.Shared
{
    public partial class NavMenu
    {
        private bool _open = true;

        private void ToggleDrawer()
        {
            _open = !_open;
        }

    }
}
