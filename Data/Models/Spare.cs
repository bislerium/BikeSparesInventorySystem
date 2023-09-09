namespace BikeSparesInventorySystem.Data.Models;

public class Spare : IModel, ICloneable
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; }

    public string Description { get; set; }

    public Guid CategoryId { get; set; }

    public decimal Price { get; set; }

    public int AvailableQuantity { get; set; }

    public object Clone()
    {
        return new Spare
        {
            Id = Id,
            Name = Name,
            Description = Description,
            CategoryId = CategoryId,
            Price = Price,
            AvailableQuantity = AvailableQuantity
        };
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
