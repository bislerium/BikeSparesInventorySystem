namespace BikeSparesInventorySystem.Data.Models;

internal class Session
{
	public Guid UserId { get; set; }
	public DateTime CreatedAt { get; set; }

	// Default session validity = 8 Hours
	public TimeSpan ValidPeriod { get; set; } = TimeSpan.FromHours(8);

	public bool IsValid() => DateTime.Now <= CreatedAt.Add(ValidPeriod);
}
