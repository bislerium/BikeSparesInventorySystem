namespace BikeSparesInventorySystem.Data.Models;

public class Sales : IModel, ICloneable
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid OrderId { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public object Clone()
    {
        return new Sales()
        {
            Id = Id,
            OrderId = OrderId,
            Name = Name,
            CreatedAt = CreatedAt,
        };
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
