using Microsoft.EntityFrameworkCore;
using open_life_server.V1.Goals.HabitGoals;
using open_life_server.V1.Goals.ListGoals;
using open_life_server.V1.Goals.NumberGoals;
using open_life_server.V1.Users;

namespace open_life_server.V1
{
    public class OpenLifeContext : DbContext
    {
        public OpenLifeContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HabitGoal>().HasMany(g => g.Logs);
            modelBuilder.Entity<HabitGoal>().HasOne<User>().WithMany().HasForeignKey(g => g.UserId);

            modelBuilder.Entity<ListGoal>().HasMany(g => g.Items);
            modelBuilder.Entity<ListGoal>().HasOne<User>().WithMany().HasForeignKey(g => g.UserId);

            modelBuilder.Entity<NumberGoal>().HasMany(g => g.Logs);
            modelBuilder.Entity<NumberGoal>().HasOne<User>().WithMany().HasForeignKey(g => g.UserId);
        }

        public DbSet<HabitGoal> HabitGoals { get; set; }
        public DbSet<HabitLog> HabitLogs { get; set; }

        public DbSet<ListGoal> ListGoals { get; set; }
        public DbSet<ListItem> ListItems { get; set; }

        public DbSet<NumberGoal> NumberGoals { get; set; }
        public DbSet<NumberLog> NumberLogs { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
