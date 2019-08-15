using Microsoft.EntityFrameworkCore;
using open_life_server.V1.Goals.HabitGoals;
using open_life_server.V1.Goals.ListGoals;
using open_life_server.V1.Goals.NumberGoals;

namespace open_life_server.V1.Goals
{
    public class GoalsContext : DbContext
    {
        public GoalsContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HabitGoal>().HasMany(h => h.Logs);
            modelBuilder.Entity<ListGoal>().HasMany(l => l.Items);
            modelBuilder.Entity<NumberGoal>().HasMany(n => n.Logs);
        }

        public DbSet<HabitGoal> HabitGoals { get; set; }
        public DbSet<HabitLog> HabitLogs { get; set; }

        public DbSet<ListGoal> ListGoals { get; set; }
        public DbSet<ListItem> ListItems { get; set; }

        public DbSet<NumberGoal> NumberGoals { get; set; }
        public DbSet<NumberLog> NumberLogs { get; set; }
    }
}
