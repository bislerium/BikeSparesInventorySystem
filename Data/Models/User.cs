using BikeSparesInventorySystem.Data.Enums;
using System.Text.Json;

namespace BikeSparesInventorySystem.Data.Models
{
    public class User : IModel
    {
        public Guid Id { get; set; } = new();
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string PasswordHash { get; set; }
        public UserRole Role { get; set; }
        public bool HasInitialPassword { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public Guid CreatedBy { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
