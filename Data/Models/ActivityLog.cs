using System.Text.Json;

namespace BikeSparesInventorySystem.Data.Models;

internal class ActivityLog : IModel
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid SpareID { get; set; }

    public int Quantity { get; set; }

    public Guid TakenBy { get; set; }

    public Guid ApprovedBy { get; set; }

    public DateTime TakenOut { get; set; }

    public override string ToString() => JsonSerializer.Serialize(this);
}
