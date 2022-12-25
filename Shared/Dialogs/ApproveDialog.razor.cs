using BikeSparesInventorySystem.Data.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BikeSparesInventorySystem.Shared.Dialogs
{
    public partial class ApproveDialog
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }

        [Parameter] public Guid ActivityLogID { get; set; }

        private ActivityLog activityLog;
        private Spare spare;

        protected sealed override void OnInitialized()
        {
            activityLog = ActivityLogRepository.Get(x => x.Id, ActivityLogID);
            spare = SpareRepository.Get(x => x.Id, activityLog.SpareID);
        }

        private void Cancel()
        {
            MudDialog.Cancel();
        }

        private void Approve()
        {
            activityLog.ApprovalStatus = Data.Enums.ApprovalStatus.Approve;
			activityLog.Approver = AuthService.CurrentUser.Id;
			Snackbar.Add("Approved!", Severity.Success);            
        }

        private void Disapprove()
        {
            spare.AvailableQuantity += activityLog.Quantity;
            activityLog.ApprovalStatus = Data.Enums.ApprovalStatus.Disapprove;
			activityLog.Approver = AuthService.CurrentUser.Id;
			Snackbar.Add("Disapproved!", Severity.Success);            
        }        
    }
}
