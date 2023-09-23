namespace BikeSparesInventorySystem.Pages;

public partial class Miner
{
    public const string Route = "/miner";

    private readonly bool Dense = true;
    private readonly bool Fixed_header = true;
    private readonly bool Fixed_footer = true;
    private readonly bool Hover = true;
    private bool ReadOnly = false;
    private readonly bool CanCancelEdit = true;
    private readonly bool BlockSwitch = true;
    private Miners SelectedItem;
    private Miners ElementBeforeEdit;
    private readonly TableApplyButtonPosition ApplyButtonPosition = TableApplyButtonPosition.End;
    private readonly TableEditButtonPosition EditButtonPosition = TableEditButtonPosition.End;
    private readonly TableEditTrigger EditTrigger = TableEditTrigger.EditButton;
    private string SearchString;
    private IEnumerable<Miners> Miners;
    private readonly Dictionary<Guid, bool> OrderDescTrack = new();

    [CascadingParameter] private Action<string> SetAppBarTitle { get; set; }
    [Parameter] public Action ChangeParentState { get; set; }
    private MudForm form;

    private string MinersName;
    private string CodeName;
    private decimal Price;
    private bool IsSaving = false;

    protected sealed override void OnInitialized()
    {
        SetAppBarTitle.Invoke("Today's Miners");
        Miners = MinersRepository.GetAll().Where(x => x.CreatedAt.ToString("MM/dd/yyyy") == DateTime.Now.ToString("MM/dd/yyyy") );

        if (!AuthService.IsUserAdmin())
        {
            ReadOnly = true;
        }
    }

    private async Task Submit()
    {
        await form.Validate();
        if (form.IsValid)
        {
            Miners Miner = new()
            {
                Name = MinersName,
                Code = CodeName,
                Price = Price,
            };

            MinersRepository.Add(Miner);

            if (IsSaving)
            {
                return;
            }

            IsSaving = true;
            await MinersRepository.FlushAsync();
            IsSaving = false;
        }
    }

    protected async Task Update()
    {
        await MinersRepository.FlushAsync();
        Snackbar.Add("Successfuly Updated!", Severity.Success);
    }


    private void BackupItem(object element)
    {
        ElementBeforeEdit = ((Miners)element).Clone() as Miners;
    }

    private void ResetItemToOriginalValues(object element)
    {
        ((Miners)element).Name = ElementBeforeEdit.Name;
        ((Miners)element).Code = ElementBeforeEdit.Code;
        ((Miners)element).Price = ElementBeforeEdit.Price;
    }

    private bool FilterFunc(Miners miner)
    {
        return string.IsNullOrWhiteSpace(SearchString)
            || miner.Name.ToString().Contains(SearchString, StringComparison.OrdinalIgnoreCase)
            || miner.Code.ToString().Contains(SearchString, StringComparison.OrdinalIgnoreCase)
            || miner.Price.ToString().Contains(SearchString, StringComparison.OrdinalIgnoreCase)
            || miner.CreatedAt.ToString().Contains(SearchString, StringComparison.OrdinalIgnoreCase);
    }

}
