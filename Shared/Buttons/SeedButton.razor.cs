namespace BikeSparesInventorySystem.Shared.Buttons;

public partial class SeedButton
{
    [Parameter] public Action<string> SetSeedUser { get; set; }

    private async Task SeedData()
    {
        await _seederService.SeedAsync();
        SetSeedUser.Invoke(UserRepository.Get(x => x.Id, Guid.Empty).UserName);
        Seeder.OnDebugConsoleWriteUserNames(UserRepository.GetAll());
        SnackBar.Add("Seeding Succesfull", Severity.Success);
    }
}
