using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace open_life_server.V1.Goals.ListGoals
{
    public class ListGoal : Goal
    {
        public int ListGoalId { get; set; }
        public int Target { get; set; }
        public string ListName { get; set; }
        public ICollection<ListItem> Items { get; set; }
    }

    public class ListItem
    {
        public int ListItemId { get; set; }
        public string Name { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Progress Progress { get; set; }

        public int ListGoalId { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum Progress
    {
        Completed,
        InProgress
    }
}
