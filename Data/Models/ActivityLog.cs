namespace BikeSparesInventorySystem.Data.Models
{
    internal class ActivityLog : IModel
    {
        public Guid Id { get; set; } = new();

        public int Quantity { get; set; }

        public Guid TakenBy { get; set; }

        public Guid ApprovedBy { get; set; }

        public DateTime TakenOut { get; set; }
    }
}
