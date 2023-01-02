using BikeSparesInventorySystem.Data.Enums;
using BikeSparesInventorySystem.Data.Models;
using BikeSparesInventorySystem.Shared;
using BikeSparesInventorySystem.Shared.Buttons;
using BikeSparesInventorySystem.Shared.Dialogs;
using BikeSparesInventorySystem.Shared.Layouts;
using MudBlazor;

namespace BikeSparesInventorySystem.Pages;

public partial class Inventory
{
    private readonly bool Dense = true;
    private readonly bool Fixed_header = true;
    private readonly bool Fixed_footer = true;
    private readonly bool Hover = true;
    private bool ReadOnly = false;
    private readonly bool CanCancelEdit = true;
    private readonly bool BlockSwitch = true;
    private string searchString = "";
    private Spare selectedItem1 = null;
    private Spare elementBeforeEdit = null;
    private readonly TableApplyButtonPosition ApplyButtonPosition = TableApplyButtonPosition.End;
    private readonly TableEditButtonPosition EditButtonPosition = TableEditButtonPosition.End;
    private readonly TableEditTrigger EditTrigger = TableEditTrigger.RowClick;
    private IEnumerable<Spare> Elements = new List<Spare>();
    private readonly Dictionary<Guid, bool> SpareDescTracks = new();

    protected sealed override void OnInitialized()
    {
        Elements = SpareRepository.GetAll();
        if (!GlobalState.IsUserAdmin)
        {
            ReadOnly = true;
        }
        foreach (Spare s in Elements)
        {
            SpareDescTracks.Add(s.Id, false);
        }
        MainLayout.Title = "Manage Bike Spares";
    }

    private void BackupItem(object element)
    {
        elementBeforeEdit = ((Spare)element).Clone() as Spare;
    }

    private void ResetItemToOriginalValues(object element)
    {
        ((Spare)element).Name = elementBeforeEdit.Name;
        ((Spare)element).Description = elementBeforeEdit.Description;
        ((Spare)element).Company = elementBeforeEdit.Company;
        ((Spare)element).Price = elementBeforeEdit.Price;
        ((Spare)element).AvailableQuantity = elementBeforeEdit.AvailableQuantity;
    }

    private bool FilterFunc(Spare element)
    {
        if (string.IsNullOrWhiteSpace(searchString))
            return true;
        if (element.Id.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (element.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (element.Description.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (element.Company.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (element.Price.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (element.AvailableQuantity.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        return false;
    }

    private void ShowBtnPress(Guid id)
    {
        SpareDescTracks[id] = !SpareDescTracks[id];
    }

    private bool GetShow(Guid id)
    {
        if (SpareDescTracks.ContainsKey(id))
        {
            return SpareDescTracks[id];
        }
        else
        {
            return SpareDescTracks[id] = false;
        }
    }

    private string GetLastTakenOut(Guid id)
    {
        List<ActivityLog> log = ActivityLogRepository.GetAll().Where(x => x.SpareID == id && x.ApprovalStatus == ApprovalStatus.Approve).ToList();
        return log.Count == 0 ? "N/A" : log.Max(x => x.ApprovalStatusOn).ToString();
    }

    private async Task AddDialog()
    {
        await DialogService.ShowAsync<AddSpareDialog>("Add Spare");
    }

    private void ActOnStock(Spare spare, StockAction action)
    {
        if (action == StockAction.Deduct)
        {
            if (!ApproveButton.ValidateWeekAndTime(Snackbar)) return;
            if (spare.AvailableQuantity == 0)
            {
                Snackbar.Add("Out of Stock!", Severity.Error);
                return;
            }
        }
        DialogParameters parameters = new()
        {
            { "StockAction", action },
            { "SpareID",  spare.Id},
        };
        DialogService.Show<StockActionDialog>($"{Enum.GetName(action)} Stock", parameters);
    }
}