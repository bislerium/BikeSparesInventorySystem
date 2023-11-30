namespace BikeSparesInventorySystem.Components.Dialogs;

public partial class ApproveDialog
{
    [CascadingParameter] private MudDialogInstance MudDialog { get; set; }

    [Parameter] public Action ChangeParentState { get; set; }

    [Parameter] public ActivityLog ActivityLog { get; set; }

    [Parameter] public Spare Spare { get; set; }

    private void Cancel()
    {
        MudDialog.Cancel();
    }

    private void Approve()
    {
        ActivityLog.ApprovalStatus = ApprovalStatus.Approve;
        ActivityLog.Approver = AuthService.CurrentUser.Id;
        ActivityLog.ApprovalStatusOn = DateTime.Now;

        Snackbar.Add("Approved!", Severity.Success);
        Close();
    }

    private void Disapprove()
    {
        Spare.AvailableQuantity += ActivityLog.Quantity;
        ActivityLog.ApprovalStatus = ApprovalStatus.Disapprove;
        ActivityLog.Approver = AuthService.CurrentUser.Id;

        Snackbar.Add("Disapproved!", Severity.Success);
        Close();
    }

    private void Close()
    {
        ChangeParentState.Invoke();
        MudDialog.Close();
    }
}
