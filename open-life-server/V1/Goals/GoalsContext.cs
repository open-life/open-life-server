using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using open_life_server.V1.Goals.HabitGoals;
using open_life_server.V1.Goals.ListGoals;
using open_life_server.V1.Goals.NumberGoals;

namespace open_life_server.V1.Goals
{
    public class GoalsContext : DbContext
    {
        public DbSet<HabitGoal> HabitGoals { get; set; }
        public DbSet<HabitLog> HabitLogs { get; set; }

        public DbSet<ListGoal> ListGoals { get; set; }
        public DbSet<ListItem> ListItems { get; set; }

        public DbSet<NumberGoal> NumberGoals { get; set; }
        public DbSet<NumberLog> NumberLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=goals.db");
        }
    }
}
