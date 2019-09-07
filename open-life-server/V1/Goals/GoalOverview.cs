namespace open_life_server.V1.Goals
{
    public class GoalOverview
    {
        public string Name { get; set; }
        public decimal Progress { get; set; }
        public decimal Target { get; set; }

        public int UserId { get; set; }
    }
}