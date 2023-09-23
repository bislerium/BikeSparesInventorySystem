namespace BikeSparesInventorySystem.Shared.Buttons;

public partial class SyncDataButton
{
    private bool IsSaving = false;

    private async Task SaveData()
    {
        if (IsSaving)
        {
            return;
        }

        IsSaving = true;
        await UserRepository.FlushAsync();
        await SpareRepository.FlushAsync();
        await CategoryRepository.FlushAsync();
        await ActivityLogRepository.FlushAsync();
        await WagesRepository.FlushAsync();
        await OrdersRepository.FlushAsync();
        await PurchasesRepository.FlushAsync();
        await UtilitiesRepository.FlushAsync();
        await SalesRepository.FlushAsync();
        await ExpensesRepository.FlushAsync();
        await MinersRepository.FlushAsync();
        IsSaving = false;

        Snackbar.Add("All Data Synced!", Severity.Success);
    }
}
