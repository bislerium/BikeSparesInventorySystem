using BikeSparesInventorySystem.Data.Models;
using BikeSparesInventorySystem.Shared.Layouts;
using MudBlazor;

namespace BikeSparesInventorySystem.Pages;

public partial class Users
{
	private readonly bool Dense = true;
	private readonly bool Fixed_header = true;
	private readonly bool Fixed_footer = true;
	private readonly bool Hover = true;
	private readonly bool ReadOnly = false;
	private readonly bool VanCancelEdit = true;
	private readonly bool BlockSwitch = true;
	private string searchString = "";
	private User selectedItem1 = null;
	private User elementBeforeEdit = null;
	private readonly TableApplyButtonPosition ApplyButtonPosition = TableApplyButtonPosition.End;
	private readonly TableEditButtonPosition EditButtonPosition = TableEditButtonPosition.End;
	private readonly TableEditTrigger EditTrigger = TableEditTrigger.RowClick;
	private IEnumerable<User> Elements = new List<User>();

	protected override void OnInitialized()
	{
		Elements = UserRepository.GetAll();
		MainLayout.Title = "Manage Users";
	}

	private void BackupItem(object element)
	{
		elementBeforeEdit = ((User)element).Clone() as User;
	}

	private string GetName(Guid id) => id.Equals(Guid.Empty) ? "N/A" : UserRepository.Get(x => x.Id, id).UserName;

	private void ResetItemToOriginalValues(object element)
	{
		((User)element).UserName = elementBeforeEdit.UserName;
		((User)element).Email = elementBeforeEdit.Email;
		((User)element).FullName = elementBeforeEdit.FullName;
		((User)element).Role = elementBeforeEdit.Role;
	}

	private bool FilterFunc(User element)
	{
		if (string.IsNullOrWhiteSpace(searchString))
			return true;
		if (element.Id.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
			return true;
		if (element.UserName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
			return true;
		if (element.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase))
			return true;
		if (element.FullName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
			return true;
		if (element.HasInitialPassword.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
			return true;
		if (element.CreatedAt.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
			return true;
		return false;
	}

	private async Task AddDialog()
	{
		await DialogService.ShowAsync<Shared.Dialogs.AddUserDialog>("Add User");
	}
}