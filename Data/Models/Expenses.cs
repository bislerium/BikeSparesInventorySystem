namespace BikeSparesInventorySystem.Data.Models;

public class Expenses : IModel, ICloneable
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int FixedCostId { get; set; }
    public int DirectCostId { get; set; }
    public string Name { get; set; }
    public DateTime? DateOfExpense { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public object Clone()
    {
        return new Expenses()
        {
            Id = Id,
            Name = Name,
            FixedCostId = FixedCostId,
            DirectCostId = DirectCostId,
            DateOfExpense = DateOfExpense,
            CreatedAt = CreatedAt,
        };
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
