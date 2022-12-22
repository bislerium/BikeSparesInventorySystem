using BikeSparesInventorySystem.Data.Utils;
using MudBlazor;

namespace BikeSparesInventorySystem.Shared
{
    public partial class SeedButton
    {
        private async Task SeedData()
        {
            await _seederService.SeedAsync();
            Seeder.OnDebugConsoleWriteUserNames(UserRepository.GetAll());
            SnackBar.Add("Seeding Succesfull", Severity.Success);
        }
    }
}
