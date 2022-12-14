using BikeSparesInventorySystem.Data.Models;
using BikeSparesInventorySystem.Data.Providers;
using BikeSparesInventorySystem.Data.Repositories;
using BikeSparesInventorySystem.Data.Utils;

namespace BikeSparesInventorySystem.Data.Services
{
    internal class SeederService
    {
        internal int MinUsers { get; set; } = 6;
        internal int MaxUsers { get; set; } = 10;
        internal int MinSpares { get; set; } = 12;
        internal int MaxSpares { get; set; } = 24;
        internal int MinActivityLogs { get; set; } = 30;
        internal int MaxActivityLogs { get; set; } = 80;

        private readonly FileProvider<User> _userFileProvider;
        private readonly FileProvider<Spare> _spareFileProvider;
        private readonly FileProvider<ActivityLog> _activityLogFileProvider;
        private readonly Repository<User> _userRepository;
        private readonly Repository<Spare> _spareRepository;
        private readonly Repository<ActivityLog> _activityLogRepository;

        public SeederService(FileProvider<User> userFileProvider, FileProvider<Spare> spareFileProvider, FileProvider<ActivityLog> activityLogFileProvider, Repository<User> userRepository, Repository<Spare> spareRepository, Repository<ActivityLog> activityLogRepository)
        {
            _userFileProvider = userFileProvider;
            _spareFileProvider = spareFileProvider;
            _activityLogFileProvider = activityLogFileProvider;
            _userRepository = userRepository;
            _spareRepository = spareRepository;
            _activityLogRepository = activityLogRepository;
        }

        public async Task Seed()
        {
            var users = Seeder.GenerateUsers(MinUsers, MaxUsers);
            var spares = Seeder.GenerateSpares(MinSpares, MaxSpares);
            var ActivityLogs = Seeder.GenerateActivityLogs(users, spares, MinActivityLogs, MaxActivityLogs);
            await _userFileProvider.WriteAsync(users);
            await _spareFileProvider.WriteAsync(spares);
            await _activityLogFileProvider.WriteAsync(ActivityLogs);
            await _userRepository.LoadAsync();
            await _spareRepository.LoadAsync();
            await _activityLogRepository.LoadAsync();
        }
    }
}
