using BikeSparesInventorySystem.Data.Enums;
using BikeSparesInventorySystem.Data.Models;
using Bogus;

namespace BikeSparesInventorySystem.Data.Utils;

internal static class Seeder
{
    public static ICollection<User> GenerateUsers(int min, int max)
    {
        List<Guid> adminIDs = new();
        DateTime dateTime = DateTime.Now.Subtract(TimeSpan.FromDays(365));
        Faker<User> userFaker = new Faker<User>()
            .RuleFor(x => x.FullName, y => y.Name.FullName())
            .RuleFor(x => x.Role, (y, z) => adminIDs.Count == 0 ? UserRole.Admin : y.PickRandom<UserRole>())
            .RuleFor(x => x.CreatedAt, y => dateTime = y.Date.Between(dateTime, DateTime.Now))
            .RuleFor(x => x.CreatedBy, y => adminIDs.Count == 0 ? Guid.Empty : y.PickRandom(adminIDs))
            .FinishWith((x, y) =>
            {
                string[] segment = y.FullName.Split(' ');
                y.UserName = x.Internet.UserName(segment[0], segment[1]);
                y.Email = x.Internet.Email(segment[0], segment[1]);
                y.PasswordHash = Hasher.HashSecret(y.UserName);
                if (y.Role == UserRole.Admin)
                {
                    adminIDs.Add(y.Id);
                }
            });
        return userFaker.GenerateBetween(min, max).ToList();
    }

    public static ICollection<Spare> GenerateSpares(int min, int max)
    {
        Faker<Spare> spareFaker = new Faker<Spare>()
            .RuleFor(x => x.Name, y => y.Commerce.ProductName())
            .RuleFor(x => x.Description, y => y.Commerce.ProductDescription())
            .RuleFor(x => x.Company, y => y.Company.CompanyName())
            .RuleFor(x => x.Price, y => y.Finance.Amount(min: 400, max: 40000))
            .RuleFor(x => x.AvailableQuantity, y => y.Random.Int(min = 0, max = 3456));
        return spareFaker.GenerateBetween(min, max);
    }

    public static ICollection<ActivityLog> GenerateActivityLogs(ICollection<User> users, ICollection<Spare> spares, int min, int max)
    {
        DateTime endDate = DateTime.Now;
        DateTime startDate = endDate.Subtract(TimeSpan.FromDays(365 * 2));
        List<User> adminUsers = users.Where(x => x.Role == UserRole.Admin).ToList();
        Faker<ActivityLog> activityLogFaker = new Faker<ActivityLog>()
            .RuleFor(x => x.SpareID, y => y.PickRandom(spares).Id)
            .RuleFor(x => x.Quantity, y => y.Random.Int(min = 1, max = 111))
            .RuleFor(x => x.ActionOn, y => y.Date.Between(startDate, endDate))
            .FinishWith((x, y) =>
            {
                User user = x.PickRandom(users);
                if (user.Role == UserRole.Admin)
                {
                    y.Action = x.PickRandom<StockAction>();
                    y.ApprovalStatus = ApprovalStatus.Approve;
                    y.Approver = user.Id;
                    y.ApprovalStatusOn = x.Date.Between(y.ActionOn, endDate);
                }
                else
                {
                    y.Action = StockAction.Deduct;
                    y.ApprovalStatus = x.PickRandom<ApprovalStatus>();
                    y.Approver = y.ApprovalStatus == ApprovalStatus.Pending ? Guid.Empty : x.PickRandom(adminUsers).Id;
                    y.ApprovalStatusOn = y.ApprovalStatus == ApprovalStatus.Pending ? default : x.Date.Between(y.ActionOn, endDate);
                }
                y.ActedBy = user.Id;
            });
        return activityLogFaker.GenerateBetween(min, max);
    }

    public static void OnDebugConsoleWriteUserNames(ICollection<User> collection)
    {
        foreach (User i in collection)
        {
            System.Diagnostics.Debug.WriteLine($"{i.UserName} | {i.HasInitialPassword} | {i.Role}");
        }
    }
}
