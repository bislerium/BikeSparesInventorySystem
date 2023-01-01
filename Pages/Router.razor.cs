using BikeSparesInventorySystem.Data.Enums;
using BikeSparesInventorySystem.Data.Services;
using BikeSparesInventorySystem.Shared;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BikeSparesInventorySystem.Pages;

public partial class Router
{
	protected sealed override void OnInitialized()
	{
		GlobalState.IsUserAdmin = AuthService.CurrentUser.Role == UserRole.Admin;
		if (AuthService.CurrentUser.HasInitialPassword)
		{
			Snackbar.Add("Please change your password!", Severity.Warning);
			NavigationManager.NavigateTo("/change-password");
		}
		else
		{
			if (GlobalState.IsUserAdmin)
			{
				NavigationManager.NavigateTo("/dashboard");
			}
			else
			{
				NavigationManager.NavigateTo("/inventory");
			}
		}
	}
}
