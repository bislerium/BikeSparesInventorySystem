namespace BikeSparesInventorySystem.Shared.Dialogs;

public partial class AddPurchaseDialog
{

    [CascadingParameter] public MudDialogInstance MudDialog { get; set; } = default!;
    [Parameter] public Action ChangeParentState { get; set; }
    [Inject] public ISnackbar Snackbar { get; set; }

    private MudForm form;

    private string Name;
    private string Vendor;
    private int Unit;
    private decimal Amount;
    private int Items;
    private DateTime ? Acquired;
    private bool IsSaving = false;

    private async Task AddPurchase()
    {
        await form.Validate();
        if (form.IsValid)
        {
            try
            {
                Purchases purchases = new()
                {
                    Name = Name,
                    Vendor = Vendor,
                    Units = Unit,
                    Amount = Amount,
                    Items = Items,
                    Acquired = Acquired
                };

                Sales sales = new()
                {
                    Purchases = Amount,
                    PurchaseDate = DateTime.Now,
                };

                PurchaseRepository.Add(purchases);
                await PurchaseRepository.FlushAsync();
                SalesRepository.Add(sales);
                await SalesRepository.FlushAsync();
                ChangeParentState.Invoke();
                IsSaving = true;

                Snackbar.Add($"{Name} is added!", Severity.Success);
                MudDialog.Close();
            }
            finally 
            {
                IsSaving = false;
            }
        }
    }

    private void Cancel() => MudDialog.Cancel();

}
