namespace BikeSparesInventorySystem.Shared
{
    public partial class NavMenu
    {
        private bool _open = true;

        void ToggleDrawer()
        {
            _open = !_open;
        }

    }
}
