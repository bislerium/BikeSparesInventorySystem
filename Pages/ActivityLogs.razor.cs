using BikeSparesInventorySystem.Data.Enums;
using BikeSparesInventorySystem.Data.Models;
using BikeSparesInventorySystem.Shared;
using BikeSparesInventorySystem.Shared.Layouts;
using MudBlazor;

namespace BikeSparesInventorySystem.Pages;

public partial class ActivityLogs
{
    private MudTable<ActivityLog> ActivityLogsTable;
    private readonly bool Dense = true;
    private readonly bool Fixed_header = true;
    private readonly bool Fixed_footer = true;
    private readonly bool Hover = true;
    private readonly bool ReadOnly = false;
    private string searchString = "";
    private IEnumerable<ActivityLog> Elements = new List<ActivityLog>();

    protected sealed override void OnInitialized()
    {
        Elements = GetByUserType();
        MainLayout.Title = "Inventory Activity Logs";
    }

    private ICollection<ActivityLog> GetByUserType() => GlobalState.IsUserAdmin 
        ? ActivityLogRepository.GetAll() 
        : ActivityLogRepository.GetAll().Where(x => x.ActedBy == AuthService.CurrentUser.Id).ToList();

    private Tuple<bool, string> GetUser(Guid id)
    {
        var user = UserRepository.Get(x => x.Id, id);
        return new Tuple<bool, string>(user?.Role == UserRole.Admin, user?.UserName);
    }

    private string GetSpareName(Guid id) => SpareRepository.Get(x => x.Id, id)?.Name;
    private string GetUserName(Guid id) => UserRepository.Get(x => x.Id, id)?.UserName;

    private bool FilterFunc(ActivityLog element)
    {
        if (string.IsNullOrWhiteSpace(searchString))
            return true;
        if (element.Id.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (element.SpareID.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        var spare = GetSpareName(element.SpareID);
        if (spare is not null && spare.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (element.Quantity.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (element.Action.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (element.ActedBy.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        var takenByUser = GetUserName(element.ActedBy);
        if (takenByUser is not null && takenByUser.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (element.ApprovalStatus.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (element.Approver.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        var approvedByUser = GetUserName(element.Approver);
        if (approvedByUser is not null && approvedByUser.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (element.Date.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        return false;
    }

    void Onchanged(string a)
    {
        var repo = GetByUserType();
        if (string.IsNullOrEmpty(a))
        {
            Elements = repo;
            return;
        }
        var yearMonth = a.Split('-');
        Elements = repo.Where(x => x.Date.Year == int.Parse(yearMonth[0]) && x.Date.Month == int.Parse(yearMonth[1])).ToList();
    }
}
