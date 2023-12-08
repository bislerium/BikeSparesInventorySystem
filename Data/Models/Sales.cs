namespace BikeSparesInventorySystem.Data.Models;

public class Sales : IModel, ICloneable
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid PurchaseId { get; set; }
    public Guid MinerId { get; set; }
    public decimal GrossSale { get; set; }
    public decimal DailyNetIncome { get; set; }
    public decimal WeeklyProfit { get; set; }
    public decimal MonthlyProfit { get; set; }
    public decimal Tithes { get; set; }
    public decimal Charity { get; set; }
    public decimal Expenses { get; set; }
    public decimal Car { get; set; }
    public decimal Profit { get; set; }
    public decimal SalesEntry { get; set; }
    public decimal Purchases { get; set; }
    public DateTime DailyDate { get; set; }
    public DateTime PurchaseDate { get; set; }
    public DateTime? CreatedAt { get; set; }

    public object Clone()
    {
        return new Sales()
        {
            Id = Id,
            PurchaseId = PurchaseId,
            MinerId = MinerId,
            GrossSale = GrossSale,
            DailyNetIncome = DailyNetIncome,
            Tithes = Tithes,
            Charity = Charity,
            Expenses = Expenses,
            Car = Car,
            Profit = Profit,
            SalesEntry = SalesEntry,
            Purchases = Purchases,
            DailyDate = DailyDate,
            CreatedAt = CreatedAt,
        };
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
