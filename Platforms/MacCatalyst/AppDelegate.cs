using Foundation;

namespace BikeSparesInventorySystem;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
    protected override MauiApp CreateMauiApp()
    {
        return MauiProgram.CreateMauiAppAsync();
    }
}
