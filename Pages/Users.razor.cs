using BikeSparesInventorySystem.Data.Models;
using BikeSparesInventorySystem.Shared;
using MudBlazor;

namespace BikeSparesInventorySystem.Pages
{
    public partial class Users
    {       
        private bool dense = true;
        private bool fixed_header = true;
        private bool fixed_footer = true;
        private bool hover = true;
        private bool ronly = false;
        private bool canCancelEdit = true;
        private bool blockSwitch = true;
        private string searchString = "";
        private User selectedItem1 = null;
        private User elementBeforeEdit;
        private TableApplyButtonPosition applyButtonPosition = TableApplyButtonPosition.End;
        private TableEditButtonPosition editButtonPosition = TableEditButtonPosition.End;
        private TableEditTrigger editTrigger = TableEditTrigger.RowClick;
        private IEnumerable<User> Elements = new List<User>();      
        protected override void OnInitialized()
        {
            Elements = UserRepository.GetAll();
            MainLayout.Title = "Manage Users";
        }

        private void BackupItem(object element)
        {
            elementBeforeEdit = ((User)element).Clone() as User;            
        }

        private string getName(Guid id) => id.Equals(Guid.Empty) ? "N/A" : UserRepository.Get(x => x.Id, id).UserName;        

        private void ResetItemToOriginalValues(object element)
        {            
            ((User)element).UserName = elementBeforeEdit.UserName;
            ((User)element).Email = elementBeforeEdit.Email;
            ((User)element).FullName = elementBeforeEdit.FullName;
            ((User)element).Role = elementBeforeEdit.Role;           
        }

        private bool FilterFunc(User element)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (element.Id.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.UserName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.FullName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.HasInitialPassword.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.CreatedAt.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;            
            return false;
        }
    }
}