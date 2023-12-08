namespace BikeSparesInventorySystem.Data.Models;

public class Wages : IModel, ICloneable
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public decimal Amount { get; set; }
    public decimal Percentage { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public object Clone()
    {
        return new Wages()
        {
            Id = Id,
            Name = Name,
            Percentage = Percentage,
            Amount = Amount,
            CreatedAt = CreatedAt,
        };
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
