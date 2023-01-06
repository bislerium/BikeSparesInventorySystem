namespace BikeSparesInventorySystem.Data.Models;

public class ActivityLog : IModel, ICloneable
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid SpareID { get; set; }

    public int Quantity { get; set; }

    public StockAction Action { get; set; }

    public Guid ActedBy { get; set; }

    public ApprovalStatus ApprovalStatus { get; set; } = ApprovalStatus.Pending;

    public Guid Approver { get; set; } = Guid.Empty;

    public DateTime ActionOn { get; set; } = DateTime.Now;

    public DateTime ApprovalStatusOn { get; set; }

    public object Clone()
    {
        return new ActivityLog
        {
            Id = Id,
            SpareID = SpareID,
            Quantity = Quantity,
            Action = Action,
            ActedBy = ActedBy,
            ApprovalStatus = ApprovalStatus,
            Approver = Approver,
            ActionOn = ActionOn,
            ApprovalStatusOn = ApprovalStatusOn,
        };
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
