namespace BikeSparesInventorySystem.Data.Models;

public class Purchases : IModel, ICloneable
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public int Units { get; set; }
    public int Items { get; set; }
    public decimal Amount { get; set; }
    public DateTime ? Acquired { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public object Clone()
    {
        return new Purchases()
        {
            Id = Id,
            Name = Name,
            Units = Units,
            Items = Items,
            Amount = Amount,
            Acquired = Acquired,
            CreatedAt = CreatedAt,
        };
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
