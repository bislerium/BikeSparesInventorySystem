namespace BikeSparesInventorySystem.Data.Models;

public class Utilities : IModel, ICloneable
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public object Clone()
    {
        return new Utilities()
        {
            Id = Id,
            Name = Name,
            Amount = Amount,
            CreatedAt = CreatedAt
        };
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
