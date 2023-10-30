namespace BikeSparesInventorySystem.Shared.Dialogs;

public partial class AddFixedCost
{
    [CascadingParameter] public MudDialogInstance Dialog {  get; set; }
    [Parameter] public Action ChangeParentState { get; set; }

    private MudForm mudForm;
    private string error { get; set; }
    private bool errorStatus;
    private int numberValue;
    private string name {  get; set; }
    private int fixedCost { get; set; }
    private int directCost { get; set; }
    private DateTime? dateOfExpense { get; set; }

    private async Task AddExpense()
    {
        await mudForm.Validate();
        if(mudForm.IsValid)
        {
            if (directCost == 0)
            {
                errorStatus = true;
                error = "Expenses should not be 0 \uD83D\uDE01";
            }
            else
            {
                Expenses expenses = new()
                {
                    Name = name,
                    FixedCostId = fixedCost,
                    DirectCostId = directCost,
                    DateOfExpense = dateOfExpense
                };

                ExpensesRepository.Add(expenses);
                ChangeParentState.Invoke();
                await ExpensesRepository.FlushAsync();

                Dialog.Close();
            }

        }
    }

    private void Cancel () => Dialog.Cancel();
}
