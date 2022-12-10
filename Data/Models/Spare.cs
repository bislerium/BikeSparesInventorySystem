namespace BikeSparesInventorySystem.Data.Models
{
    internal class Spare : IModel
    {
        public Guid Id { get; set; } = new();

        public string Name { get; set; }

        public string Description { get; set; }

        public string Company { get; set; }

        public decimal Price { get; set; }

        public int AvailableQuantity { get; set; }
    }
}
