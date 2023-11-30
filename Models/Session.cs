namespace BikeSparesInventorySystem.Models;

public class Session
{
    public Guid UserId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public TimeSpan ValidPeriod { get; set; }

    public bool KeepAlive { get; set; }


    // Keep the session alive to auto-login without ever having to login.
    // Else the session is only valid for next 8 hours after login and has to re-login afterward. 
    public static Session Generate(Guid userID, bool keepAlive = false)
    {
        return new Session()
        {
            UserId = userID,
            KeepAlive = keepAlive,
            ValidPeriod = keepAlive ? default : TimeSpan.FromHours(8),
        };
    }
    public static Session Generate(Guid userID, TimeSpan validPeriod)
    {
        return new Session()
        {
            UserId = userID,
            ValidPeriod = validPeriod,
        };
    }

    public bool IsValid()
    {
        return KeepAlive || (DateTime.Now <= CreatedAt.Add(ValidPeriod));
    }
}
