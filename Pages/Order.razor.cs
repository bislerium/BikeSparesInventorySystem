namespace BikeSparesInventorySystem.Pages;

public partial class Order
{
    public const string Route = "/orders";

    //private readonly bool Dense = true;
    //private readonly bool Fixed_Header = true;
    //private readonly bool Fixed_Footer = true;
    //private readonly bool Hover = true;
    //private bool ReadOnly = false;
    //private readonly bool CanCancelEdit = true;
    //private readonly bool BlockSwitch = true;
    //private Orders SelectedItems;
    //private Orders ElementBeforeEdit;
    //private readonly TableApplyButtonPosition ApplyButtonPosition = TableApplyButtonPosition.End;
    //private readonly TableEditButtonPosition EditButtonPosition = TableEditButtonPosition.End;
    //private readonly TableEditTrigger EditTrigger = TableEditTrigger.RowClick;
    //private string SearchString;
    //private IEnumerable<Orders> Elements;
    //private List<Spare> Products;
    //private List<Miners> Miners;
    //private readonly Dictionary<Guid, bool> OrderDescTrack = new();


    [CascadingParameter] private Action<string> SetAppBarTitle { get; set; }

    //protected sealed override void OnInitialized()
    //{
    //    SetAppBarTitle.Invoke("Manage Orders");
    //    Elements = OrdersRepository.GetAll();
    //}

    //private (string productName, string minerName) GetOrdersById(Guid id)
    //{
    //    var order = Elements.FirstOrDefault(x => x.Id == id);
    //    if (order == null)
    //    {
    //        throw new ArgumentException($"order found with the ID {id}");
    //    }

    //    var product = Products.FirstOrDefault(x => x.Id);
    //}

}
