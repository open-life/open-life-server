using Microsoft.EntityFrameworkCore;

namespace open_life_server.V1.Users
{
    public class UsersContext : DbContext
    {
        public UsersContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
