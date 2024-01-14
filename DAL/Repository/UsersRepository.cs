using Entitles.Models;

namespace DAL.Repository
{
    public class UsersRepository
    {
        public async Task<IEnumerable<ApplicationUser>> FindAsync(Func<ApplicationUser, bool> predicate, string? userId = null)
        {
            using var _db = new ApplicationContext();
            var users = _db.Users.Where(predicate).ToList();
            var res = await Task.FromResult(users);
            return res;
        }
    }
}
