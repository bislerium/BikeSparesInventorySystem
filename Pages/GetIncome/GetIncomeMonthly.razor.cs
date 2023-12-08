//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace BikeSparesInventorySystem.Pages.GetIncome;

//public partial class GetIncomeMonthly
//{
//    public const string Route = "/dailyIncome";

//    private readonly bool Dense = true;
//    private readonly bool Fixed_header = true;
//    private readonly bool Fixed_footer = true;
//    private readonly bool Hover = true;
//    private bool ReadOnly = false;
//    private readonly bool CanCancelEdit = true;
//    private readonly bool BlockSwitch = true;
//    private Sales SelectedItem;
//    private Sales ElementBeforeEdit;
//    private readonly TableApplyButtonPosition ApplyButtonPosition = TableApplyButtonPosition.End;
//    private readonly TableEditButtonPosition EditButtonPosition = TableEditButtonPosition.End;
//    private readonly TableEditTrigger EditTrigger = TableEditTrigger.EditButton;
//    protected Guid categoryId;
//    protected int count;
//    private string SearchString;
//    private IEnumerable<Sales> _Sales;

//    [CascadingParameter]
//    private Action<string> SetAppBarTitle { get; set; }

//    public Guid _purchaseId { get; set; }
//    public Guid _expenseId { get; set; }

//    protected sealed override void OnInitialized()
//    {
//        SetAppBarTitle.Invoke("Manage Sales");
//        _Sales = SalesRepository.GetAll().OrderBy(s => s.DailyDate);
//    }

//    private bool FilterFunc(Sales sales)
//    {
//        return string.IsNullOrWhiteSpace(SearchString)
//        || sales.Id.ToString().Contains(SearchString, StringComparison.OrdinalIgnoreCase)
//        || sales.DailyDate.ToString("MM/dd/yyyy").Contains(SearchString, StringComparison.OrdinalIgnoreCase)
//        || sales.PurchaseDate.ToString("MM/dd/yyyy").Contains(SearchString, StringComparison.OrdinalIgnoreCase)
//        || sales.DailyNetIncome.ToString().Contains(SearchString, StringComparison.OrdinalIgnoreCase)
//        || sales.Purchases.ToString().Contains(SearchString, StringComparison.OrdinalIgnoreCase)
//        || sales.GrossSale.ToString().Contains(SearchString, StringComparison.OrdinalIgnoreCase)
//        || sales.Profit.ToString().Contains(SearchString, StringComparison.OrdinalIgnoreCase);
//    }

//    private void FilterByMonth(string a)
//    {
//        ICollection<Sales> _sales = SalesRepository.GetAll();
//        if (string.IsNullOrEmpty(a))
//        {
//            _Sales = _sales;
//            return;
//        }
//        string[] date = a.Split('-');
//        _Sales = _sales.Where(d => d.DailyDate.Year == int.Parse(date[0]) && d.DailyDate.Month == int.Parse(date[1])).ToList();
//    }
//}

//}
