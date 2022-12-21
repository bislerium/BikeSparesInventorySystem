using BikeSparesInventorySystem.Data.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BikeSparesInventorySystem.Shared.Dialogs
{
    public partial class ApproveDialog
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }

        [Parameter] public Guid ActivityID { get; set; }

        private ActivityLog activityLog;
        private Spare spare;

        protected override void OnInitialized()
        {
            activityLog = ActivityLogRepository.Get(x => x.Id, ActivityID);
            spare = SpareRepository.Get(x => x.Id, activityLog.SpareID);
        }

        private string errorMessage = null;        

        private void Cancel()
        {
            MudDialog.Cancel();
        }

        private void Approve()
        {
            if (Validate()) {
                activityLog.Approver = AuthService.CurrentUser.Id;
                Snackbar.Add("Approved!", Severity.Success);
            }
        }

        private void Disapprove()
        {
            if (Validate())
            {
                spare.AvailableQuantity += activityLog.Quantity;
                Snackbar.Add("Disapproved!", Severity.Success);
            }
        }

        // private ActivityLog GetActivityLog() => ActivityLogRepository.Get(x => x.Id, ActivityID);

        private bool Validate()
        {
            var currentDateTime = DateTime.Now;
            var currentTime = currentDateTime.TimeOfDay;
            var currentDate = currentDateTime.Date;
            if (currentDate.DayOfWeek == DayOfWeek.Saturday || currentDate.DayOfWeek == DayOfWeek.Sunday)
            {
                errorMessage = "Approval outside 9AM to 4PM restricted!";
            }
            if (currentTime.Hours < 9 && currentTime.Hours > 12 + 4)
            {
                errorMessage = "Approval in weekends restricted!";
            }
            MudDialog.Close();
            if (errorMessage is not null) { 
                Snackbar.Add(errorMessage, Severity.Error);
                errorMessage = null;
                return false;
            }
            return true;
        }
    }
}
