using BikeSparesInventorySystem.Data.Enums;
using BikeSparesInventorySystem.Data.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BikeSparesInventorySystem.Shared.Dialogs
{
    public partial class StockActionDialog
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Parameter] public StockAction StockAction { get; set; }
        [Parameter] public Guid SpareID { get; set; }
        private Spare Spare { get; set; }
        private MudBlazor.Color ButtonColor { get; set; }
        private MudForm Form { get; set; }
        private int Quantity { get; set; } = 1;
        private int MaxQuantity { get; set; }

        protected sealed override void OnInitialized()
        {
            Spare = SpareRepository.Get(x => x.Id, SpareID);
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
                var user = AuthService.CurrentUser;
                ActivityLog ac = new()
                {
                    SpareID = SpareID,
                    Quantity = Quantity,
                    Action = StockAction,
                    ActedBy = user.Id,
                };

                if (user.Role == UserRole.Admin)
                {
                    ac.ApprovalStatus = ApprovalStatus.Approve;
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
                Snackbar.Add($"Spare {Spare.Name} stock's {Enum.GetName(StockAction).ToLower()}ed by {Quantity}!", Severity.Info);
                MudDialog.Close();
            }
        }


        private void Cancel() => MudDialog.Cancel();
    }
}
