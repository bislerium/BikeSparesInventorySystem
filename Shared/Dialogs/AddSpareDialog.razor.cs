using BikeSparesInventorySystem.Data.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BikeSparesInventorySystem.Shared.Dialogs;

public partial class AddSpareDialog
{
	[CascadingParameter] MudDialogInstance MudDialog { get; set; }
	MudForm form;

	private string Name;
	private string Description;
	private string Company;
	private decimal Price;
	private int AvailableQuantity;

	private void Cancel()
	{
		MudDialog.Cancel();
	}

	private async Task AddSpare()
	{
		await form.Validate();
		if (form.IsValid)
		{
			Spare spare = new()
			{
				Name = Name,
				Description = Description,
				Company = Company,
				Price = Price,
				AvailableQuantity = AvailableQuantity
			};
			SpareRepository.Add(spare);
			Snackbar.Add($"Spare {Name} is Added!", Severity.Success);
			MudDialog.Close();
		}
	}
}
