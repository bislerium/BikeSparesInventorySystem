using BikeSparesInventorySystem.Data.Enums;
using BikeSparesInventorySystem.Data.Models;
using BikeSparesInventorySystem.Shared;
using Microsoft.AspNetCore.Components;
using MudBlazor;

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
        if (element.TakenBy.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        var takenByUser = GetUserName(element.TakenBy);
        if (takenByUser is not null  && takenByUser.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (element.ApprovedBy.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        var approvedByUser = GetUserName(element.ApprovedBy);
        if (approvedByUser is not null && approvedByUser.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (element.TakenOut.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        return false;
    }

    private async Task Approve(Guid id)
    {
        var currentDateTime = DateTime.Now;
        var currentTime = currentDateTime.TimeOfDay;
        var currentDate = currentDateTime.Date;
        string errorMessage = null;
        if (!(currentDate.DayOfWeek == DayOfWeek.Saturday || currentDate.DayOfWeek == DayOfWeek.Sunday))
        {
            if (currentTime.Hours >= 9 && currentTime.Hours <= 12 + 4)
            {
                var log = ActivityLogRepository.Get(x => x.Id, id);
                var parameters = new DialogParameters
                    {
                        { "ContentText", $"{GetSpareName(log.SpareID)} is going to be stocked out by {log.Quantity}, Do you really want to approve?" },
                        { "ButtonText", "Approve" },
                        { "Color", MudBlazor.Color.Warning }
                    };

                var options = new DialogOptions() { CloseOnEscapeKey = true };

                var dialog = await DialogService.ShowAsync<Dialog>("Approve", parameters, options);
                var result = await dialog.Result;

                if (!result.Cancelled)
                {
                    log.ApprovedBy = AuthService.CurrentUser.Id;
                    Snackbar.Add("Approved!", Severity.Success);
                }
                return;
            } else
            {
                errorMessage = "Approval outside 9AM to 4PM restricted!";
            }
        } else
        {
            errorMessage = "Approval in weekends restricted!";
        }
        Snackbar.Add(errorMessage, Severity.Error);
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
