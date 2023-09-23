namespace BikeSparesInventorySystem.Data.Models;

public class Expenses : IModel, ICloneable
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid FixedCostId { get; set; }
    public Guid DirectCostId { get; set; }

    public object Clone()
    {
        return new Expenses()
        {
            Id = Id,
            FixedCostId = FixedCostId,
            DirectCostId = DirectCostId
        };
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
