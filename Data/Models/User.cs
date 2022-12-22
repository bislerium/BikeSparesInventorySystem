using BikeSparesInventorySystem.Data.Enums;
using System.Text.Json;

namespace BikeSparesInventorySystem.Data.Models;

public class User : IModel, ICloneable
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string UserName { get; set; }
    public string Email { get; set; }
    public string FullName { get; set; }
    public string PasswordHash { get; set; }
    public UserRole Role { get; set; }
    public bool HasInitialPassword { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public Guid CreatedBy { get; set; }

    public object Clone()
    {
        return new User
        {
            Id = Id,
            UserName = UserName,
            Email = Email,
            FullName = FullName,
            PasswordHash = PasswordHash,
            Role = Role,
            HasInitialPassword = HasInitialPassword,
            CreatedAt = CreatedAt,
            CreatedBy = CreatedBy,
        };
    }

    public override string ToString() => JsonSerializer.Serialize(this);
}
