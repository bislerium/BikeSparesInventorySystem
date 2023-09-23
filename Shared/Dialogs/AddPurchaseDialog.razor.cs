namespace BikeSparesInventorySystem.Shared.Dialogs;

public partial class AddPurchaseDialog
{

    [CascadingParameter] public MudDialogInstance MudDialog { get; set; } = default!;
    [Parameter] public Action ChangeParentState { get; set; }
    [Inject] public ISnackbar Snackbar { get; set; }

    private MudForm form;

    private string Name;
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
                    Units = Unit,
                    Amount = Amount,
                    Items = Items,
                    Acquired = Acquired
                };

                PurchaseRepository.Add(purchases);
                await PurchaseRepository.FlushAsync();
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
