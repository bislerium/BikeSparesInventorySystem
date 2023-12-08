//namespace BikeSparesInventorySystem.Data.Models;

//public class WeeklyIncome
//{
//    public Guid Id { get; set; } = Guid.NewGuid();
//    public decimal GrossSale { get; set; }
//    public decimal DailyNetIncome { get; set; }
//    public decimal Tithes { get; set; }
//    public decimal Charity { get; set; }
//    public decimal Expenses { get; set; }
//    public decimal Car { get; set; }
//    public decimal Profit { get; set; }
//    public decimal Purchases { get; set; }
//    public DateTime DailyDate { get; set; }
//    public DateTime PurchaseDate { get; set; }
//    public DateTime? CreatedAt { get; set; }

//    public object Clone()
//    {
//        return new DailyIncome()
//        {
//            Id = Id,
//            GrossSale = GrossSale,
//            DailyNetIncome = DailyNetIncome,
//            Tithes = Tithes,
//            Charity = Charity,
//            Expenses = Expenses,
//            Car = Car,
//            Profit = Profit,
//            Purchases = Purchases,
//            DailyDate = DailyDate,
//            CreatedAt = CreatedAt,
//        };
//    }

//    public override string ToString()
//    {
//        return JsonSerializer.Serialize(this);
//    }
//}
