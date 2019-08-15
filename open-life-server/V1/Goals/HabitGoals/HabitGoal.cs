using System;
using System.Collections.Generic;

namespace open_life_server.V1.Goals.HabitGoals
{
    public class HabitGoal
    {
        public int HabitGoalId { get; set; }
        public string Name { get; set; }
        public int Target { get; set; }
        public List<HabitLog> Logs { get; set; }
    }

    public class HabitLog
    {
        public int HabitLogId { get; set; }
        public DateTime Date { get; set; }
        public bool HabitCompleted { get; set; }

        public int HabitGoalId { get; set; }
        public HabitGoal HabitGoal { get; set; }
    }
}
