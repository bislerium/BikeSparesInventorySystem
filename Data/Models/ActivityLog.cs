using BikeSparesInventorySystem.Data.Enums;
using System.Text.Json;

namespace BikeSparesInventorySystem.Data.Models;

internal class ActivityLog : IModel, ICloneable
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid SpareID { get; set; }

    public int Quantity { get; set; }

    public InventoryAction Action { get; set; }

    public Guid ActedBy { get; set; }

    public ApprovalStatus ApprovalStatus { get; set; } 

    public Guid Approver { get; set; }

    public DateTime TakenOut { get; set; }

    public object Clone()
    {
        return new ActivityLog
        {
            Id = Id,
            SpareID = SpareID,
            Quantity = Quantity,
            Action= Action,
            ActedBy = ActedBy,
            ApprovalStatus= ApprovalStatus,
            Approver = Approver,
            TakenOut = TakenOut,
        };
    }

    public override string ToString() => JsonSerializer.Serialize(this);
}
