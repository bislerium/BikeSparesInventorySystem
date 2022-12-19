using BikeSparesInventorySystem.Data.Enums;
using BikeSparesInventorySystem.Data.Repositories;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BikeSparesInventorySystem.Shared.Dialogs
{
    public partial class CreateUserDialog
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        MudForm form;
        bool success;

        private string UserName;
        private string Email;
        private string FullName;
        private string Role;


        private void Cancel()
        {
            MudDialog.Cancel();
        }

        private async Task AddUser()
        {
            await form.Validate();
            if (form.IsValid)
            {
                AuthService.Register(UserName, Email, FullName, Enum.Parse<UserRole>(Role));
                Snackbar.Add($"User {UserName} is Added!", Severity.Success);
                MudDialog.Close();
            }
        }

        private IEnumerable<string> UserNameValidation(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                yield return "Password is required!";
                yield break;
            }
            if (UserRepository.HasUserName(arg))
                yield return "Username already Exist!";
        }
    }
}
