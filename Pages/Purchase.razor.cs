namespace BikeSparesInventorySystem.Pages;

public partial class Purchase
{
    public const string Route = "/purchase";

    private readonly bool Dense = true;
    private readonly bool Fixed_header = true;
    private readonly bool Fixed_footer = true;
    private readonly bool Hover = true;
    private bool ReadOnly = false;
    private readonly bool CanCancelEdit = true;
    private readonly bool BlockSwitch = true;
    private string SearchString;
    private Purchases SelectedItem;
    private Purchases ElementBeforeEdit;    

    private readonly TableApplyButtonPosition ApplyButtonPosition = TableApplyButtonPosition.End;
    private readonly TableEditButtonPosition EditButtonPosition = TableEditButtonPosition.End;
    private readonly TableEditTrigger EditTrigger = TableEditTrigger.EditButton;
    private IEnumerable<Purchases> Elements;

    [CascadingParameter]
    private Action<string> SetAppBarTitle { get; set; }

    protected sealed override void OnInitialized()
    {
        SetAppBarTitle.Invoke("Manage Purchase");
        Elements = PurchaseRepository.GetAll();
    }

    protected async Task Update()
    {
        await PurchaseRepository.FlushAsync();
        Snackbar.Add("Updated Successfuly", Severity.Success);
    }

    private void BackupItem(object element)
    {
        ElementBeforeEdit = ((Purchases)element).Clone() as Purchases;
    }

    private void ResetItemToOriginalValues(object element)
    {
        ((Purchases)element).Name = ElementBeforeEdit.Name;
        ((Purchases)element).Amount = ElementBeforeEdit.Amount;
        ((Purchases)element).Units = ElementBeforeEdit.Units;
        ((Purchases)element).Acquired = ElementBeforeEdit.Acquired;
    }

    private bool FilterFunc(Purchases element)
    {
        return string.IsNullOrWhiteSpace(SearchString)
               || element.Id.ToString().Contains(SearchString, StringComparison.OrdinalIgnoreCase)
               || element.Name.Contains(SearchString, StringComparison.OrdinalIgnoreCase)
               || element.Acquired.ToString().Contains(SearchString, StringComparison.OrdinalIgnoreCase)
               || element.Units.ToString().Contains(SearchString, StringComparison.OrdinalIgnoreCase);
    }

    private async Task ShowAddPurchase()
    {
        DialogParameters parameters = new()
        {
            { "ChangeParentState", new Action(StateHasChanged)}
        };
        await DialogService.ShowAsync<AddPurchaseDialog>("Add Purchase", parameters);

    }
}
