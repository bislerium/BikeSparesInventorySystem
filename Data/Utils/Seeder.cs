
using BikeSparesInventorySystem.Data.Enums;
using BikeSparesInventorySystem.Data.Models;
using Bogus;
using System;

namespace BikeSparesInventorySystem.Data.Utils
{
    internal static class Seeder
    {
        public static ICollection<User> GenerateUsers(int min, int max)
        {
            var adminIDs = new List<Guid>();
            DateTime dateTime = DateTime.Now.Subtract(TimeSpan.FromDays(365));
            var userFaker = new Faker<User>()
                .RuleFor(x => x.Id, y => Guid.NewGuid())
                .RuleFor(x => x.FullName, y => y.Name.FullName())                
                .RuleFor(x => x.UserName, (y, z) =>
                {
                    var segment = z.FullName.Split(' ');
                    return y.Internet.UserName(segment[0], segment[1]);
                })
                .RuleFor(x => x.Email, (y, z) =>
                {
                    var segment = z.FullName.Split(' ');
                    return y.Internet.Email(segment[0], segment[1]);
                })
                .RuleFor(x => x.PasswordHash, (y, z) => Hasher.HashSecret(z.UserName))
                .RuleFor(x => x.Role, (y, z) => {
                    var role = y.PickRandom<UserRole>();
                    if (role == UserRole.Admin) adminIDs.Add(z.Id);
                    return role;
                    })
                .RuleFor(x => x.CreatedAt, y => dateTime = y.Date.Between(dateTime ,DateTime.Now))
                .RuleFor(x => x.CreatedBy, y => y.PickRandom(adminIDs));

                return userFaker.GenerateBetween(min, max).ToList();
        }

        public static ICollection<Spare> GenerateSpares(int min, int max)
        {
            var spareFaker = new Faker<Spare>()
                .RuleFor(x => x.Id, y => Guid.NewGuid())
                .RuleFor(x => x.Name, y => y.Lorem.Word())
                .RuleFor(x => x.Description, y => y.Lorem.Sentences(5))
                .RuleFor(x => x.Company, y => y.Company.CompanyName())
                .RuleFor(x => x.Price, y => y.Finance.Amount(min: 900, max: 40000))
                .RuleFor(x => x.AvailableQuantity, y => y.Random.Int(max: 3456));
            return spareFaker.GenerateBetween(min, max);
        }

        public static ICollection<ActivityLog> GenerateActivityLogs(ICollection<User> users, ICollection<Spare> spares, int min, int max)
        {
            var adminUsers = users.Where(x => x.Role == UserRole.Admin).ToList();
            var activityLogFaker = new Faker<ActivityLog>()
                .RuleFor(x => x.Id, y => Guid.NewGuid())
                .RuleFor(x => x.SpareID, y => y.PickRandom(spares).Id)
                .RuleFor(x => x.Quantity, y => y.Random.Int(min:1, max: 111))
                .RuleFor(x => x.TakenBy, y => y.PickRandom(users).Id)
                .RuleFor(x => x.ApprovedBy, y => y.PickRandom(adminUsers).Id)
                .RuleFor(x => x.TakenOut, y => y.Date.Recent());
            return activityLogFaker.GenerateBetween(min, max);
        }
    }
}
