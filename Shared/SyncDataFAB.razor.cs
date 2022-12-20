using MudBlazor;

namespace BikeSparesInventorySystem.Shared;

public partial class SyncDataFAB
{
    private bool IsSaving = false;
    private async Task SaveData()
    {   
        if (IsSaving) return;
        IsSaving = true;
        await UserRepository.FlushAsync();
        await SpareRepository.FlushAsync();
        await ActivityLogRepository.FlushAsync();
        IsSaving = false;
        Snackbar.Add("All Data Synced!", Severity.Success);
    }
}
