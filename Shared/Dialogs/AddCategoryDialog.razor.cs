namespace BikeSparesInventorySystem.Shared.Dialogs;

public partial class AddCategoryDialog
{
    [CascadingParameter] public MudDialogInstance MudDialog { get; set; }
    [Parameter] public Action ChangeParentState {  get; set; }

    private MudForm form;

    private string Name;

    private async Task Submit()
    {
        await form.Validate();
        if (form.IsValid)
        {
            Category category = new()
            {
                Name = Name,
            };

            CategoryRepository.Add(category);
            ChangeParentState.Invoke();

            snackBar.Add($"{Name} is added!", Severity.Success);
            MudDialog.Close();
        }
    }

    private void Cancel() => MudDialog.Cancel();
}
