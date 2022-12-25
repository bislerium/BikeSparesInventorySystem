namespace BikeSparesInventorySystem.Pages;

public partial class Index
{
    protected sealed override async Task OnInitializedAsync()
    {
        await _userRepository.LoadAsync();
        await _spareRepository.LoadAsync();
        await _activityLogRepository.LoadAsync();
        await Task.Delay(1000);
        if (_authService.CurrentUser is null)
        {
            _navigationManager.NavigateTo("/login");
        }
        else
        {
			_navigationManager.NavigateTo("/router");
        }
    }
}