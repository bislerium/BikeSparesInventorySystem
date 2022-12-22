using BikeSparesInventorySystem.Data.Models;
using BikeSparesInventorySystem.Shared;
using MudBlazor;

namespace BikeSparesInventorySystem.Pages;

public partial class Inventory
{
    private bool dense = true;
    private bool fixed_header = true;
    private bool fixed_footer = true;
    private bool hover = true;
    private bool ronly = false;
    private bool canCancelEdit = true;
    private bool blockSwitch = true;
    private string searchString = "";
    private Spare selectedItem1 = null;
    private Spare elementBeforeEdit;
    private TableApplyButtonPosition applyButtonPosition = TableApplyButtonPosition.End;
    private TableEditButtonPosition editButtonPosition = TableEditButtonPosition.End;
    private TableEditTrigger editTrigger = TableEditTrigger.RowClick;
    private IEnumerable<Spare> Elements = new List<Spare>();
    private readonly Dictionary<Guid, bool> SpareDescTracks = new();

    protected override void OnInitialized()
    {
        Elements = SpareRepository.GetAll();
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

    private void ShowBtnPress(Guid id) => SpareDescTracks[id] = !SpareDescTracks[id];

    private bool getShow(Guid id)
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

    private string getLastTakenOut(Guid id)
    {
        var log = ActivityLogRepository.GetAll().Where(x => x.SpareID == id).ToList();
        return log.Count == 0 ? "N/A" : log.Max(x => x.TakenOut).ToString();
    }

    private async Task AddDialog()
    {
        await DialogService.ShowAsync<Shared.Dialogs.AddSpareDialog>("Add Spare");
    }
}