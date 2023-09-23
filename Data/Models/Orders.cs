namespace BikeSparesInventorySystem.Data.Models;

public class Orders : IModel, ICloneable
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ProductId { get; set; }
    public Guid MinerId { get; set; }
    public bool IsCancelled { get; set; }
    public bool IsDelivered { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;


    public object Clone()
    {
        return new Orders
        {
            Id = Id,
            ProductId = ProductId,
            MinerId = MinerId,
            IsCancelled = IsCancelled,
            IsDelivered = IsDelivered,
            CreatedAt = CreatedAt,
        };
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }

}