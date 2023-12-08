namespace BikeSparesInventorySystem.Pages;

public partial class GetWages
{

    public const string Route = "/getWages";

    private IEnumerable<Sales> _sales;
    private IEnumerable<Wages> _wages;

    [CascadingParameter]
    private Action<string> SetAppBarTitle { get; set; }

    protected sealed override void OnInitialized()
    {
        SetAppBarTitle.Invoke("Manage Monthly Wages");
        _wages = WagesRepository.GetAll();
    } 

    protected async Task LoadData()
    {
        _sales = SalesRepository.GetAll();

        Wages wages = new Wages()
        {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                
        };
    }

}
