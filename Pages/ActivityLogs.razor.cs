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

    private ICollection<ActivityLog> GetByUserType()
    {
        return GlobalState.IsUserAdmin
        ? ActivityLogRepository.GetAll()
        : ActivityLogRepository.GetAll().Where(x => x.ActedBy == AuthService.CurrentUser.Id).ToList();
    }

    private Tuple<bool, string> GetUser(Guid id)
    {
        User user = UserRepository.Get(x => x.Id, id);
        return new Tuple<bool, string>(user?.Role == UserRole.Admin, user?.UserName);
    }

    private string GetSpareName(Guid id)
    {
        return SpareRepository.Get(x => x.Id, id)?.Name;
    }

    private string GetUserName(Guid id)
    {
        return UserRepository.Get(x => x.Id, id)?.UserName;
    }

    private bool FilterFunc(ActivityLog element)
    {
        if (string.IsNullOrWhiteSpace(searchString))
            return true;
        if (element.Id.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (element.SpareID.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        string spare = GetSpareName(element.SpareID);
        if (spare is not null && spare.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (element.Quantity.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (element.Action.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (element.ActedBy.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        string takenByUser = GetUserName(element.ActedBy);
        if (takenByUser is not null && takenByUser.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (element.ApprovalStatus.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (element.Approver.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        string approvedByUser = GetUserName(element.Approver);
        if (approvedByUser is not null && approvedByUser.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (element.ActionOn.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        return false;
    }

    private void Onchanged(string a)
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
