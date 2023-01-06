namespace BikeSparesInventorySystem.Shared.Dialogs;

public partial class StockActionDialog
{
    [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
    [Parameter] public StockAction StockAction { get; set; }
    [Parameter] public Spare Spare { get; set; }
    [Parameter] public Action ChangeParentState { get; set; }
    private MudBlazor.Color ButtonColor { get; set; }
    private MudForm Form { get; set; }
    private int Quantity { get; set; } = 1;
    private int MaxQuantity { get; set; }

    protected sealed override void OnInitialized()
    {
        switch (StockAction)
        {
            case StockAction.Add:
                ButtonColor = MudBlazor.Color.Primary;
                MaxQuantity = 100000;
                break;
            case StockAction.Deduct:
                ButtonColor = MudBlazor.Color.Secondary;
                MaxQuantity = Spare.AvailableQuantity;
                break;
        }
    }

    private void Submit()
    {
        Form.Validate();

        if (Form.IsValid)
        {
            User user = AuthService.CurrentUser;
            ActivityLog ac = new()
            {
                SpareID = Spare.Id,
                Quantity = Quantity,
                Action = StockAction,
                ActedBy = user.Id,
            };

            if (user.Role == UserRole.Admin)
            {
                ac.ApprovalStatus = ApprovalStatus.Approve;
                ac.ApprovalStatusOn = DateTime.Now;
                ac.Approver = user.Id;
            }
            switch (StockAction)
            {
                case StockAction.Add:
                    Spare.AvailableQuantity += Quantity;
                    break;
                case StockAction.Deduct:
                    Spare.AvailableQuantity -= Quantity;
                    break;
            }
            ActivityLogRepository.Add(ac);
            ChangeParentState.Invoke();

            Snackbar.Add($"Spare {Spare.Name} stock's {Enum.GetName(StockAction).ToLower()}ed by {Quantity}!", Severity.Info);
            MudDialog.Close();
        }
    }


    private void Cancel()
    {
        MudDialog.Cancel();
    }
}
