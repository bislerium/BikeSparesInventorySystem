using BikeSparesInventorySystem.Data.Services;
using Microsoft.AspNetCore.Components;

namespace BikeSparesInventorySystem.Pages
{
    public partial class Index
    {        
    
        protected override void OnInitialized()
        {
            if (_authService.CurrentUser == null)
            {
                _navigationManager.NavigateTo("/login");
            }
            else
            {
                _navigationManager.NavigateTo("/inventory");
            }
        }
    }
}