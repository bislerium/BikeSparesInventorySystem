using BikeSparesInventorySystem.Data.Enums;
using BikeSparesInventorySystem.Data.Repositories;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Text.RegularExpressions;

namespace BikeSparesInventorySystem.Shared.Dialogs;

public partial class AddUserDialog
{
    [CascadingParameter] MudDialogInstance MudDialog { get; set; }
    MudForm form;

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
            yield return "Username is required!";
            yield break;
        }
        if (!Regex.Match(arg, @"^[a-zA-Z0-9]([._-](?![._-])|[a-zA-Z0-9]){3,18}[a-zA-Z0-9]$").Success)
        {
            yield return "Invalid Username";
        }
        if (UserRepository.HasUserName(arg))
            yield return "Username already Exist!";
    }
}
