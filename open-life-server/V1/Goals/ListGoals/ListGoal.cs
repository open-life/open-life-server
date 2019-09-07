using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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

        [JsonConverter(typeof(ProgressConverter))]
        public Progress Progress { get; set; }

        public int ListGoalId { get; set; }
    }

    [JsonConverter(typeof(ProgressConverter))]
    public enum Progress
    {
        Completed,
        InProgress
    }

    public class ProgressConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var messageTransportResponseStatus = (Progress)value;

            switch (messageTransportResponseStatus)
            {
                case Progress.Completed:
                    writer.WriteValue("Completed");
                    break;
                case Progress.InProgress:
                    writer.WriteValue("In Progress");
                    break;
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var enumString = (string)reader.Value;

            return Enum.Parse(typeof(Progress), Regex.Replace(enumString, @"\s+", ""), true);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }
    }
}
