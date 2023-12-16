namespace BikeSparesInventorySystem.Components.Pages;

public partial class ActivityLogs
{
    public const string Route = "/activity-logs";

    private readonly bool Dense = true;
    private readonly bool Fixed_header = true;
    private readonly bool Fixed_footer = true;
    private readonly bool Hover = true;
    private readonly bool ReadOnly = false;
    private string SearchString;
    private IEnumerable<ActivityLog> Elements;

    [CascadingParameter]
    private Action<string> SetAppBarTitle { get; set; }

    protected sealed override void OnInitialized()
    {
        SetAppBarTitle.Invoke("Inventory Activity Logs");
        Elements = GetByUserType();
    }

    private ICollection<ActivityLog> GetByUserType()
    {
        return AuthService.IsUserAdmin()
        ? ActivityLogRepository.GetAll()
        : ActivityLogRepository.GetAll().Where(x => x.ActedBy == AuthService.CurrentUser.Id).ToList();
    }

    private Tuple<bool, string> GetUser(Guid id)
    {
        User? user = UserRepository.Get(x => x.Id, id);
        return new Tuple<bool, string>(user?.Role == UserRole.Admin, user.UserName);
    }

    private string? GetSpareName(Guid id)
    {
        return SpareRepository.Get(x => x.Id, id)?.Name;
    }

    private string? GetUserName(Guid id)
    {
        return UserRepository.Get(x => x.Id, id)?.UserName;
    }

    private bool FilterFunc(ActivityLog element)
    {
        if (string.IsNullOrWhiteSpace(SearchString))
        {
            return true;
        }

        if (element.Id.ToString().Contains(SearchString, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        if (element.SpareID.ToString().Contains(SearchString, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        string? spare = GetSpareName(element.SpareID);
        if (spare is not null && spare.Contains(SearchString, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        if (element.Quantity.ToString().Contains(SearchString, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        if (element.Action.ToString().Contains(SearchString, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        if (element.ActedBy.ToString().Contains(SearchString, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        string? takenByUser = GetUserName(element.ActedBy);
        if (takenByUser is not null && takenByUser.Contains(SearchString, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        if (element.ApprovalStatus.ToString().Contains(SearchString, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        if (element.Approver.ToString().Contains(SearchString, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        string? approvedByUser = GetUserName(element.Approver);
        return (approvedByUser is not null && approvedByUser.Contains(SearchString, StringComparison.OrdinalIgnoreCase))
               || element.ActionOn.ToString().Contains(SearchString, StringComparison.OrdinalIgnoreCase);
    }

    private void FilterByMonth(string a)
    {
        ICollection<ActivityLog> repo = GetByUserType();
        if (string.IsNullOrEmpty(a))
        {
            Elements = repo;
            return;
        }
        string[] yearMonth = a.Split('-');
        Elements = repo.Where(x => x.ApprovalStatusOn.Year == int.Parse(yearMonth[0]) && x.ActionOn.Month == int.Parse(yearMonth[1])).ToList();
    }
}
