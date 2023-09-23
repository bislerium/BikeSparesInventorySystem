namespace BikeSparesInventorySystem.Pages;

public partial class Index
{
    protected sealed override async Task OnInitializedAsync()
    {
        await _userRepository.LoadAsync();
        await _spareRepository.LoadAsync();
        await _activityLogRepository.LoadAsync();
        await _categoryRepository.LoadAsync();
        await _ordersRepository.LoadAsync();
        await _wagesRepository.LoadAsync();
        await _minersRepository.LoadAsync();
        await _purchasesRepository.LoadAsync();
        await _utilitiesRepository.LoadAsync();
        await _expensesRepository.LoadAsync();
        await _salesRepository.LoadAsync();
        try
        {
            await _authService.CheckSession();
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, MudBlazor.Severity.Error);
        }

        await Task.Delay(1000);

        if (_authService.CurrentUser is null)
        {
            _navigationManager.NavigateTo(Login.Route);
        }
        else
        {
            _navigationManager.NavigateTo(RoleRouter.Route);
        }
    }
}