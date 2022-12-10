
using BikeSparesInventorySystem.Data.Enums;
using BikeSparesInventorySystem.Data.Models;
using BikeSparesInventorySystem.Data.Providers;
using Bogus;

namespace BikeSparesInventorySystem.Data.Utils
{
    internal class Seeder
    {
        internal int MinUsers { get; set; } = 6;
        internal int MaxUsers { get; set; } = 10;
        internal int MinSpares { get; set; } = 12;
        internal int MaxSpares { get; set; } = 24;
        internal int MinActivityLogs { get; set; } = 30;
        internal int MaxActivityLogs { get; set; } = 80;

        private readonly IFileProvider<User> _userFileProvider;
        private readonly IFileProvider<Spare> _spareFileProvider;
        private readonly IFileProvider<ActivityLog> _activityLogFileProvider;

        public Seeder(IFileProvider<User> userFileProvider, IFileProvider<Spare> spareFileProvider, IFileProvider<ActivityLog> activityLogFileProvider)
        {
            _userFileProvider = userFileProvider;
            _spareFileProvider = spareFileProvider;
            _activityLogFileProvider = activityLogFileProvider;
        }

        public void Seed() {
            var users = GenerateUsers(MinUsers, MaxUsers);
            var spares = GenerateSpares(MinSpares, MaxSpares);
            var ActivityLogs = GenerateActivityLogs(users, spares, MinActivityLogs, MaxActivityLogs);
            _userFileProvider.Write(users);
            _spareFileProvider.Write(spares);
            _activityLogFileProvider.Write(ActivityLogs);
        }        

        public static ICollection<User> GenerateUsers(int min, int max)
        {
            var adminIDs = new List<Guid>();
            DateTime dateTime = DateTime.Now.Subtract(TimeSpan.FromDays(365));
            var userFaker = new Faker<User>()
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
                .RuleFor(x => x.SpareID, y => y.PickRandom(spares).Id)
                .RuleFor(x => x.Quantity, y => y.Random.Int(min:1, max: 111))
                .RuleFor(x => x.TakenBy, y => y.PickRandom(users).Id)
                .RuleFor(x => x.ApprovedBy, y => y.PickRandom(adminUsers).Id)
                .RuleFor(x => x.TakenOut, y => y.Date.Recent());
            return activityLogFaker.GenerateBetween(min, max);
        }
    }
}
