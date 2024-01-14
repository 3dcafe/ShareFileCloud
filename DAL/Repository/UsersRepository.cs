using Entitles.Models;
using Microsoft.Extensions.Logging;

namespace DAL.Repository
{
    public class UsersRepository
    {
        private readonly ILogger _logger;
        /// <summary>
        /// Constructor for the users repository.
        /// </summary>
        /// <param name="logger">Logger interface for recording information about events.</param>
        public UsersRepository(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<IEnumerable<ApplicationUser>> FindAsync(Func<ApplicationUser, bool> predicate, string? userId = null)
        {
            using var _db = new ApplicationContext();
            var users = _db.Users.Where(predicate).ToList();
            var res = await Task.FromResult(users);
            return res;
        }

        public async Task<ApplicationUser?> CreateAsync(ApplicationUser model, string userId)
        {
            try
            {
                model.LastLogin = DateTimeOffset.UtcNow;
                using var _db = new ApplicationContext();
                if (_db.Users == null)
                {
                    _logger.LogError("Error in UsersRepository->CreateAsync table not found", $"Error table [Users] not found");
                    return null;
                }
                _db.Users.Add(model);
                await _db.SaveChangesAsync();
                return model;
            }
            catch (Exception ex)
            {
                // Logging errors to track issues during the database query execution
                _logger.LogError("Error in UsersRepository->CreateAsync", $"Error in CreateAsync: {ex.Message}");
                return null;
            }
        }
    }
}
