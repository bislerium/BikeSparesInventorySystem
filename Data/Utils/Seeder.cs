using BikeSparesInventorySystem.Data.Enums;
using BikeSparesInventorySystem.Data.Models;
using Bogus;

namespace BikeSparesInventorySystem.Data.Utils;

internal static class Seeder
{
    public static ICollection<User> GenerateUsers(int min, int max)
    {
        var adminIDs = new List<Guid>();
        DateTime dateTime = DateTime.Now.Subtract(TimeSpan.FromDays(365));
        var userFaker = new Faker<User>()                
            .RuleFor(x => x.FullName, y => y.Name.FullName())
            .RuleFor(x => x.Role, (y, z) => adminIDs.Count == 0 ? UserRole.Admin : y.PickRandom<UserRole>())
            .RuleFor(x => x.CreatedAt, y => dateTime = y.Date.Between(dateTime ,DateTime.Now))
            .RuleFor(x => x.CreatedBy, y => adminIDs.Count == 0 ? Guid.Empty : y.PickRandom(adminIDs))                
            .FinishWith((x,y) => {
                var segment = y.FullName.Split(' ');
                y.UserName = x.Internet.UserName(segment[0], segment[1]);
                y.Email = x.Internet.Email(segment[0], segment[1]);
                y.PasswordHash = Hasher.HashSecret(y.UserName);
					if (y.Role == UserRole.Admin) adminIDs.Add(y.Id);
				});
        return userFaker.GenerateBetween(min, max).ToList();
    }

    public static ICollection<Spare> GenerateSpares(int min, int max)
    {
        var spareFaker = new Faker<Spare>()                
            .RuleFor(x => x.Name, y => y.Lorem.Word())
            .RuleFor(x => x.Description, y => y.Lorem.Sentences(5))
            .RuleFor(x => x.Company, y => y.Company.CompanyName())
            .RuleFor(x => x.Price, y => y.Finance.Amount(min: 400, max: 40000))
            .RuleFor(x => x.AvailableQuantity, y => y.Random.Int(min = 0, max = 3456));
        return spareFaker.GenerateBetween(min, max);
    }

    public static ICollection<ActivityLog> GenerateActivityLogs(ICollection<User> users, ICollection<Spare> spares, int min, int max)
    {
        var adminUsers = users.Where(x => x.Role == UserRole.Admin).ToList();
        var activityLogFaker = new Faker<ActivityLog>()
            .RuleFor(x => x.SpareID, y => y.PickRandom(spares).Id)
            .RuleFor(x => x.Quantity, y => y.Random.Int(min = 1, max = 111))
            .RuleFor(x => x.TakenBy, y => y.PickRandom(users).Id)
            .RuleFor(x => x.ApprovedBy, y => y.PickRandom(adminUsers).Id)
            .RuleFor(x => x.TakenOut, y => y.Date.Recent());
        return activityLogFaker.GenerateBetween(min, max);
    }
}
