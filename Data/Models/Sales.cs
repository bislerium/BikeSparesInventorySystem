namespace BikeSparesInventorySystem.Data.Models;

public class Sales : IModel, ICloneable
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid PurchaseId { get; set; }
    public Guid MinerId { get; set; }
    public decimal GrossSale { get; set; }
    public decimal NetSale { get; set; }
    public decimal Tithes { get; set; }
    public decimal Charity { get; set; }
    public decimal Expenses { get; set; }
    public decimal Car { get; set; }
    public decimal Profit { get; set; }
    public decimal SalesEntry { get; set; }
    public decimal Purchases { get; set; }
    public DateTime MinersDate { get; set; }
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
            NetSale = NetSale,
            Tithes = Tithes,
            Charity = Charity,
            Expenses = Expenses,
            Car = Car,
            Profit = Profit,
            SalesEntry = SalesEntry,
            Purchases = Purchases,
            MinersDate = MinersDate,
            CreatedAt = CreatedAt,
        };
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
