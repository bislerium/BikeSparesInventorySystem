using BikeSparesInventorySystem.Data.Enums;

namespace BikeSparesInventorySystem.Data.Models
{
    public class User : IModel
    {
        public Guid Id { get; set; } = new();
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public UserRole Role { get; set; }
        public bool HasInitialPassword { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public Guid CreatedBy { get; set; }
    }
}
