namespace BikeSparesInventorySystem.Components.Pages;

public partial class Users
{
    public const string Route = "/users";

    private readonly bool Dense = true;
    private readonly bool Fixed_header = true;
    private readonly bool Fixed_footer = true;
    private readonly bool Hover = true;
    private readonly bool ReadOnly = false;
    private readonly bool VanCancelEdit = true;
    private readonly bool BlockSwitch = true;
    private string SearchString;
    private User ElementBeforeEdit;
    private readonly TableApplyButtonPosition ApplyButtonPosition = TableApplyButtonPosition.End;
    private readonly TableEditButtonPosition EditButtonPosition = TableEditButtonPosition.End;
    private readonly TableEditTrigger EditTrigger = TableEditTrigger.RowClick;
    private IEnumerable<User> Elements;

    [CascadingParameter]
    private Action<string> SetAppBarTitle { get; set; }

    protected override void OnInitialized()
    {
        SetAppBarTitle.Invoke("Manage Users");
        Elements = UserRepository.GetAll();
    }

    private void BackupItem(object element)
    {
        ElementBeforeEdit = (User)((User)element).Clone();
    }

    private string GetUserName(Guid id)
    {
        var username = UserRepository.Get(x => x.Id, id)?.UserName;
        return username is null ? "N/A" : username;
    }

    private void ResetItemToOriginalValues(object element)
    {
        ((User)element).UserName = ElementBeforeEdit.UserName;
        ((User)element).Email = ElementBeforeEdit.Email;
        ((User)element).FullName = ElementBeforeEdit.FullName;
        ((User)element).Role = ElementBeforeEdit.Role;
    }

    private bool FilterFunc(User element)
    {
        return string.IsNullOrWhiteSpace(SearchString)
               || element.Id.ToString().Contains(SearchString, StringComparison.OrdinalIgnoreCase)
               || element.UserName.Contains(SearchString, StringComparison.OrdinalIgnoreCase)
               || element.Email.Contains(SearchString, StringComparison.OrdinalIgnoreCase)
               || element.FullName.Contains(SearchString, StringComparison.OrdinalIgnoreCase)
               || element.HasInitialPassword.ToString().Contains(SearchString, StringComparison.OrdinalIgnoreCase)
               || element.CreatedAt.ToString().Contains(SearchString, StringComparison.OrdinalIgnoreCase);
    }

    private async Task AddDialog()
    {
        DialogParameters parameters = new()
        {
            { "ChangeParentState", new Action(StateHasChanged) }
        };
        await DialogService.ShowAsync<AddUserDialog>("Add User", parameters);
    }

    private void Reload()
    {
        StateHasChanged();
    }
}