namespace BikeSparesInventorySystem.Pages.GetIncome;

public partial class GetIncomeDaily
{
    public const string Route = "/dailyIncome";

    private readonly bool Dense = true;
    private readonly bool Fixed_header = true;
    private readonly bool Fixed_footer = true;
    private readonly bool Hover = true;
    private bool ReadOnly = false;
    private readonly bool CanCancelEdit = true;
    private readonly bool BlockSwitch = true;
    private Sales SelectedItem;
    private Sales ElementBeforeEdit;
    private readonly TableApplyButtonPosition ApplyButtonPosition = TableApplyButtonPosition.End;
    private readonly TableEditButtonPosition EditButtonPosition = TableEditButtonPosition.End;
    private readonly TableEditTrigger EditTrigger = TableEditTrigger.EditButton;
    protected Guid categoryId;
    protected int count;
    private string SearchString;
    private IEnumerable<Sales> _Sales;
    private IEnumerable<Purchases> _Purchases;
    private IEnumerable<Miners> _Miners;
    private IEnumerable<Expenses> _Expenses;

    [CascadingParameter]
    private Action<string> SetAppBarTitle { get; set; }

    public Guid _purchaseId { get; set; }
    public Guid _expenseId { get; set; }

    protected sealed override void OnInitialized()
    {
        SetAppBarTitle.Invoke("Manage Sales");
        _Sales = SalesRepository.GetAll().OrderBy(s => s.DailyDate);
        _Purchases = PurchaseRepository.GetAll();
        _Expenses = ExpensesRepository.GetAll();
    }
    private void BackupItem(object element)
    {
        ElementBeforeEdit = ((Sales)element).Clone() as Sales;
    }

    public async Task SaveInSales()
    {

        _Miners = MinersRepository.GetAll();

        var grossSalesPerDay = _Miners
            .Where(x => x.Status == "Delivered")
            .GroupBy(x => x.DateUpdated.Date)  // Group by date without considering time
            .Select(group => new
            {
                Date = group.Key,
                GrossSale = group.Sum(x => x.Price),
                Id = group.Select(x => x.Id).FirstOrDefault(),
            })
            .OrderBy(entry => entry.Date);

        foreach (var saleData in grossSalesPerDay)
        {
            if (!_Sales.Any(x => x.DailyDate == saleData.Date) && !_Sales.Any(x => x.Id == saleData.Id))
            {
                Sales sales = new Sales()
                {
                    GrossSale = saleData.GrossSale,
                    DailyDate = saleData.Date
                };

                SalesRepository.Add(sales);
                await SalesRepository.FlushAsync();
                Snackbar.Add("Sales is updated!", Severity.Info);
            }
            else if (_Sales.Any(x => x.DailyDate == saleData.Date) && !_Sales.Any(x => x.Id == saleData.Id))
            {
                var existing = SalesRepository.Get(x => x.DailyDate, saleData.Date);
                if (existing != null)
                {
                    existing.GrossSale = saleData.GrossSale;
                    existing.DailyNetIncome = existing.GrossSale - existing.Expenses - existing.Purchases;
                    existing.Tithes = existing.DailyNetIncome * (decimal).10;
                    existing.Car = existing.DailyNetIncome * (decimal).5;
                    existing.Charity = existing.DailyNetIncome * (decimal).5;
                    existing.Profit = existing.DailyNetIncome - existing.Tithes - existing.Car - existing.Charity;
                }
                await SalesRepository.FlushAsync();
                Snackbar.Add("Sales is up to date!", Severity.Info);
            }
            else
            {
                Snackbar.Add("Sales is up to date!", Severity.Info);
            }
        }
    }

    protected async Task Update()
    {
        var grossSale = SalesRepository.Get(x => x.Id, SelectedItem.Id)
                        .GrossSale;
        var purchase = _Purchases.Where(x => x.Id == _purchaseId)
                    .Select(x => x.Amount).FirstOrDefault();

        var expenses = _Expenses.Where(x => x.Id == _expenseId)
                    .Select(x => x.DirectCostId).FirstOrDefault();

        var netSale = grossSale - purchase - expenses;
        var Tithes = (decimal).10 * netSale;
        var Charity = (decimal).5 * netSale;
        var Car = (decimal).5 * netSale;

        var existingSales = SalesRepository.Get(x => x.Id, SelectedItem.Id);

        if (existingSales != null)
        {
            existingSales.Purchases = purchase;
            existingSales.Expenses = expenses;
            existingSales.Tithes = (decimal).10 * netSale;
            existingSales.Charity = (decimal).5 * netSale;
            existingSales.Car = (decimal).5 * netSale;
            existingSales.DailyNetIncome = netSale;
            existingSales.Profit = netSale - Tithes - Charity - Car;
        }
        await SalesRepository.FlushAsync();
    }

    private bool FilterFunc(Sales sales)
    {
        return string.IsNullOrWhiteSpace(SearchString)
        || sales.Id.ToString().Contains(SearchString, StringComparison.OrdinalIgnoreCase)
        || sales.DailyDate.ToString("MM/dd/yyyy").Contains(SearchString, StringComparison.OrdinalIgnoreCase)
        || sales.PurchaseDate.ToString("MM/dd/yyyy").Contains(SearchString, StringComparison.OrdinalIgnoreCase)
        || sales.DailyNetIncome.ToString().Contains(SearchString, StringComparison.OrdinalIgnoreCase)
        || sales.Purchases.ToString().Contains(SearchString, StringComparison.OrdinalIgnoreCase)
        || sales.GrossSale.ToString().Contains(SearchString, StringComparison.OrdinalIgnoreCase)
        || sales.Profit.ToString().Contains(SearchString, StringComparison.OrdinalIgnoreCase);
    }

    private void FilterByMonth(string a)
    {
        ICollection<Sales> _sales = SalesRepository.GetAll();
        if (string.IsNullOrEmpty(a))
        {
            _Sales = _sales;
            return;
        }
        string[] date = a.Split('-');
        _Sales = _sales.Where(d => d.DailyDate.Year == int.Parse(date[0]) && d.DailyDate.Month == int.Parse(date[1])).ToList();
    }
}
