using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BikeSparesInventorySystem.Shared.Buttons
{
    public partial class ApproveButton
    {
        [Parameter] public Guid ActivityLogID { get; set; }

        internal static bool ValidateWeekAndTime(ISnackbar snackbar)
        {
            DateTime currentDateTime = DateTime.Now;
            TimeSpan currentTime = currentDateTime.TimeOfDay;
            DateTime currentDate = currentDateTime.Date;

            string errorMessage = null;

            if (currentDate.DayOfWeek == DayOfWeek.Saturday || currentDate.DayOfWeek == DayOfWeek.Sunday)
            {
                errorMessage = "Action on weekends restricted!";
            }
            if (currentTime.Hours < 9 || currentTime.Hours > 12 + 4)
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
            if (ValidateWeekAndTime(Snackbar))
            {
                DialogParameters parameters = new()
                {
                    { "ActivityLogID", ActivityLogID }
                };
                await DialogService.ShowAsync<Dialogs.ApproveDialog>("Approval", parameters);
            }
        }
    }
}
