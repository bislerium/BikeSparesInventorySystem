namespace BikeSparesInventorySystem.Pages;

public partial class Order
{
    public const string Route = "/orders";

    private readonly bool Dense = true;
    private readonly bool Fixed_header = true;
    private readonly bool Fixed_footer = true;
    private readonly bool Hover = true;
    private bool ReadOnly = false;
    private readonly bool CanCancelEdit = true;
    private readonly bool BlockSwitch = true;
    private HashSet<Miners> SelectedItems { get; set; } = new HashSet<Miners>();
    private string SelectedStatus = string.Empty;
    private Miners SelectedItem;
    private Miners ElementBeforeEdit;
    private readonly TableApplyButtonPosition ApplyButtonPosition = TableApplyButtonPosition.End;
    private readonly TableEditButtonPosition EditButtonPosition = TableEditButtonPosition.End;
    private readonly TableEditTrigger EditTrigger = TableEditTrigger.EditButton;
    private string SearchString;
    private IEnumerable<Miners> Miners;
    private readonly Dictionary<Guid, bool> OrderDescTrack = new();
    bool IsSelectionActive => SelectedItems?.Any() ?? false;

    [CascadingParameter] private Action<string> SetAppBarTitle { get; set; }
    [Parameter] public Action ChangeParentState { get; set; }
    private MudForm form;

    protected sealed override void OnInitialized()
    {
        SetAppBarTitle.Invoke("Today's Miners");
        Miners = MinersRepository.GetAll();

        if (!AuthService.IsUserAdmin())
        {
            ReadOnly = true;
        }

    }

    private async Task UpdateStatus()
    {
        if (SelectedItems != null && SelectedItems.Any())
        {

            foreach(var item in SelectedItems)
            {
                item.Status = SelectedStatus;
            }

        }

        await MinersRepository.FlushAsync();
        Snackbar.Add("Successfuly Updated!", Severity.Success);
    }

    protected void Close() => SelectedItems?.Clear();

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

    private void FilterByDate(string i)
    {
        ICollection<Miners> miners = MinersRepository.GetAll();
        if (string.IsNullOrEmpty(i))
        {
            Miners = miners;
            return;
        }
        string[] date = i.Split('-');

        if(date.Length != 3)
        {
            return;
        }

        int year = int.Parse(date[0]);
        int month = int.Parse(date[1]);
        int day = int.Parse(date[2]);

        Miners = miners.Where(d => d.CreatedAt.Year == year && d.CreatedAt.Month == month && d.CreatedAt.Day == day).ToList();
    }

}
