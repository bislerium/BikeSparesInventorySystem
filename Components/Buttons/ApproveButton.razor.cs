namespace BikeSparesInventorySystem.Components.Buttons;

public partial class ApproveButton
{
    [Parameter] public ActivityLog ActivityLog { get; set; }
    [Parameter] public Action ChangeParentState { get; set; }

    internal static bool ValidateWeekAndTime(ISnackbar snackbar)
    {
        DateTime currentDateTime = DateTime.Now;
        TimeSpan currentTime = currentDateTime.TimeOfDay;
        DateTime currentDate = currentDateTime.Date;

        string errorMessage = null;

        if (currentDate.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
        {
            errorMessage = "Action on weekends restricted!";
        }
        if (currentTime.Hours is < 9 or > (12 + 4))
        {
            errorMessage = "Action outside 9AM to 4PM restricted!";
        }

        if (errorMessage is not null)
        {
            snackbar.Add(errorMessage, Severity.Error);
            return false;
        }

        return true;
    }

    private async Task Approve()
    {
        var spare = SpareRepository.Get(x => x.Id, ActivityLog.SpareID);
        if (spare == null)
        {
            Snackbar.Add("Spare not found!", Severity.Error);
            return;
        }

        if (ValidateWeekAndTime(Snackbar))
        {
            DialogParameters parameters = new()
            {
                { "Spare", spare },
                { "ActivityLog", ActivityLog },
                { "ChangeParentState", ChangeParentState }
            };
            await DialogService.ShowAsync<Dialogs.ApproveDialog>("Approval", parameters);
        }
    }
}
