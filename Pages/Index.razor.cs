﻿namespace BikeSparesInventorySystem.Pages;

public partial class Index
{
    protected sealed override async Task OnInitializedAsync()
    {
        await _userRepository.LoadAsync();
        await _spareRepository.LoadAsync();
        await _activityLogRepository.LoadAsync();
        await _categoryRepository.LoadAsync();
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