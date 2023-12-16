namespace BikeSparesInventorySystem.Components.Buttons;

public partial class SeedButton
{
    [Parameter] public Action<string> SetSeedUser { get; set; }

    private async Task SeedData()
    {
        await _seederService.SeedAsync();
        SetSeedUser.Invoke(UserRepository.Get(x => x.Id, Guid.Empty)!.UserName);
        UserRepository.OnDebugConsoleWriteUserNames();
        SnackBar.Add("Seeding Succesfull", Severity.Success);
    }
}
