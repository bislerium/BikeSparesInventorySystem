namespace BikeSparesInventorySystem.Data.Models;

public class Category : IModel, ICloneable
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid ProductId { get; set; }

    public string Name { get; set; }

    public int Quantity { get; set; }

    public Guid ActedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public object Clone()
    {
        return new Category
        {
            Id = Id,
            ProductId = ProductId,
            Name = Name,
            Quantity = Quantity,
            CreatedAt = CreatedAt,
            ActedBy = ActedBy,
        };
    }
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
