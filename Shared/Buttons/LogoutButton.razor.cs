using BikeSparesInventorySystem.Shared.Dialogs;
using Microsoft.AspNetCore.Components;
using MudBlazor;
namespace BikeSparesInventorySystem.Shared.Buttons;

public partial class LogoutButton
{
	private async Task Logout()
	{
		var parameters = new DialogParameters
		{
			{ "ContentText", "Do you really want to Logout?" },
			{ "ButtonText", "Logout" },
			{ "Color", MudBlazor.Color.Error }
		};

		var dialog = await DialogService.ShowAsync<Dialog>("Logout", parameters);
		var result = await dialog.Result;

		if (!result.Cancelled)
		{
			AuthService.LogOut();
			Snackbar.Clear();
			Snackbar.Add("Logged out!", Severity.Success);
			NavigationManager.NavigateTo("/login", replace: true);
		}
	}
}


