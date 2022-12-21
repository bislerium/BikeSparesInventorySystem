using BikeSparesInventorySystem.Data.Enums;
using BikeSparesInventorySystem.Data.Models;
using BikeSparesInventorySystem.Shared;
using MudBlazor;
using NPOI.SS.Formula.Functions;

namespace BikeSparesInventorySystem.Pages;

public partial class ActivityLogs
{
    private bool dense = true;
    private bool fixed_header = true;
    private bool fixed_footer = true;
    private bool hover = true;
    private bool ronly = false;
    private bool canCancelEdit = true;
    private string searchString = "";
    private IEnumerable<ActivityLog> Elements = new List<ActivityLog>();
    protected override void OnInitialized()
    {
        Elements = ActivityLogRepository.GetAll();
        MainLayout.Title = "Inventory Activity Logs";
    }

    private Tuple<bool,string> GetUser(Guid id)
    {
        var user = UserRepository.Get(x => x.Id, id);
        return new Tuple<bool, string>(user.Role == UserRole.Admin, user.UserName);
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
        if (element.ActedBy.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        var takenByUser = GetUserName(element.ActedBy);
        if (takenByUser is not null  && takenByUser.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (element.Approver.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        var approvedByUser = GetUserName(element.Approver);
        if (approvedByUser is not null && approvedByUser.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (element.TakenOut.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        return false;
    }

    private async Task Approve(Guid id)
    {
        var parameters = new DialogParameters
        {
            { "ActivityID", id }
        };
        await DialogService.ShowAsync<Shared.Dialogs.ApproveDialog>("Approve", parameters);
    }

    void onchanged(string a)
    {
        var repo = ActivityLogRepository.GetAll();
        if (string.IsNullOrEmpty(a))
        {
            Elements = repo;
            return;
        }
        var yearMonth = a.Split('-');
        Elements = repo.Where(x=> x.TakenOut.Year == int.Parse(yearMonth[0]) && x.TakenOut.Month == int.Parse(yearMonth[1])).ToList();
    }
    
}
