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

        private Tuple<bool,string> getUser(Guid id)
        {
            var user = UserRepository.Get(x => x.Id, id);
            return new Tuple<bool, string>(user.Role == Data.Enums.UserRole.Admin, user.UserName);
        }

    private string getSpareName(Guid id) => SpareRepository.Get(x => x.Id, id).Name;

        private bool FilterFunc(ActivityLog element)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (element.Id.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.SpareID.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.Quantity.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.TakenBy.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.ApprovedBy.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.TakenOut.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }

    private async Task approve(Guid id)
    {
        var log = ActivityLogRepository.Get(x => x.Id, id);
        var parameters = new DialogParameters
        {
            { "ContentText", $"{getSpareName(log.SpareID)} is going to be stocked out by {log.Quantity}.\n Do you really want to approve?" },
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
        
    }
    
}
