using System;
using System.Collections.Generic;

namespace open_life_server.V1.Goals.NumberGoals
{
    public class NumberGoal : Goal
    {
        public int NumberGoalId { get; set; }
        public int Target { get; set; }
        public List<NumberLog> Logs { get; set; }
    }

    public class NumberLog
    {
        public int NumberLogId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }

        public int NumberGoalId { get; set; }
        public NumberGoal NumberGoal { get; set; }
    }
}
