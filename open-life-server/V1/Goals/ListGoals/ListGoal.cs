using System.Collections.Generic;

namespace open_life_server.V1.Goals.ListGoals
{
    public class ListGoal
    {
        public int ListGoalId { get; set; }
        public string Name { get; set; }
        public int Target { get; set; }
        public string ColumnName { get; set; }
        public List<ListItem> Items { get; set; }
    }

    public class ListItem
    {
        public int ListItemId { get; set; }
        public string Name { get; set; }
        public Progress Progress { get; set; }

        public int ListGoalId { get; set; }
        public ListGoal ListGoal { get; set; }
    }

    public enum Progress
    {
        Completed,
        InProgress
    }
}
