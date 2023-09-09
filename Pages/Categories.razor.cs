namespace BikeSparesInventorySystem.Pages;

public partial class Categories
{
    public const string Route = "/category";

    private readonly bool Dense = true;
    private readonly bool Fixed_header = true;
    private readonly bool Fixed_footer = true;
    private readonly bool Hover = true;
    private bool ReadOnly = false;
    private readonly bool CanCancelEdit = true;
    private readonly bool BlockSwitch = true;
    private Category SelectedItem;
	private Category ElementBeforeEdit;
    private readonly TableApplyButtonPosition ApplyButtonPosition = TableApplyButtonPosition.End;
    private readonly TableEditButtonPosition EditButtonPosition = TableEditButtonPosition.End;
    private readonly TableEditTrigger EditTrigger = TableEditTrigger.RowClick;
	protected Guid categoryId;
	protected int count;
    private string SearchString;
    private IEnumerable<Category> Elements;
	private readonly Dictionary<Guid, bool> CategoryDescTracks = new();
	private IEnumerable<Spare> Products;

    [CascadingParameter]
    private Action<string> SetAppBarTitle { get; set; }

    protected sealed override void OnInitialized()
    {
        SetAppBarTitle.Invoke("Manage Product Category");
        Elements = CategoryRepository.GetAll();

		if (!AuthService.IsUserAdmin())
		{
			ReadOnly = true;
		}
		foreach (Category s in Elements)
		{
			CategoryDescTracks.Add(s.Id, false);
		}

		Products = SpareRepository.GetAll();
		
	}


	private void BackupItem(object element)
	{
		ElementBeforeEdit = ((Category)element).Clone() as Category;
	}

	private void ResetItemToOriginalValues(object element)
	{
		((Category)element).Name = ElementBeforeEdit.Name;
		((Category)element).CreatedAt = ElementBeforeEdit.CreatedAt;
		((Category)element).Quantity = ElementBeforeEdit.Quantity;
	}

	private bool FilterFunc(Category element)
	{
		return string.IsNullOrWhiteSpace(SearchString)
			   || element.Id.ToString().Contains(SearchString, StringComparison.OrdinalIgnoreCase)
			   || element.Name.Contains(SearchString, StringComparison.OrdinalIgnoreCase)
			   || element.CreatedAt.ToString().Contains(SearchString, StringComparison.OrdinalIgnoreCase);

	}

	private void ShowBtnPress(Guid id)
	{
		CategoryDescTracks[id] = !CategoryDescTracks[id];
	}

	private bool GetShow(Guid id)
	{
		return CategoryDescTracks.ContainsKey(id) ? CategoryDescTracks[id] : (CategoryDescTracks[id] = false);
	}

	private string GetLastTakenOut(Guid id)
	{
		List<ActivityLog> log = ActivityLogRepository.GetAll().Where(x => x.SpareID == id && x.Action == StockAction.Deduct && x.ApprovalStatus == ApprovalStatus.Approve).ToList();
		return log.Count == 0 ? "N/A" : log.Max(x => x.ApprovalStatusOn).ToString();
	}

	private async Task AddDialog()
	{
		DialogParameters parameters = new()
		{
			{ "ChangeParentState", new Action(StateHasChanged) }
		};
		await DialogService.ShowAsync<AddCategoryDialog>("Add Category", parameters);
	}

	private async Task ActOnStock(Spare spare, StockAction action)
	{
		if (action == StockAction.Deduct)
		{
			if (!ApproveButton.ValidateWeekAndTime(Snackbar))
			{
				return;
			}

			if (spare.AvailableQuantity == 0)
			{
				Snackbar.Add("Out of Stock!", Severity.Error);
				return;
			}
		}
		DialogParameters parameters = new()
		{
			{ "StockAction", action },
			{ "Spare",  spare},
			{ "ChangeParentState", new Action(StateHasChanged) }
		};
		await DialogService.ShowAsync<StockActionDialog>($"{Enum.GetName(action)} Stock", parameters);
	}

	private void FilterByMonth(string a)
	{
		ICollection<Category> repo = CategoryRepository.GetAll();
		if (string.IsNullOrEmpty(a))
		{
			Elements = repo;
			return;
		}
		string[] yearMonth = a.Split('-');
		Elements = repo.Where(x => x.CreatedAt.Year == int.Parse(yearMonth[0]) && x.CreatedAt.Month == int.Parse(yearMonth[1])).ToList();
	}
}
