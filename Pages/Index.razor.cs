namespace BikeSparesInventorySystem.Pages;

public partial class Index
{        

    protected override async Task OnInitializedAsync()
    { 
        await _userRepository.LoadAsync();
        await _spareRepository.LoadAsync();
        await _activityLogRepository.LoadAsync();

        if (_authService.CurrentUser is null)
        {
            _navigationManager.NavigateTo("/login");
        }
        else
        {
            _navigationManager.NavigateTo("/dashboard");
        }
    }
}