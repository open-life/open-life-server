using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace open_life_server.V1.Goals
{
    public interface IGoalValidator
    {
        bool Valid(Goal goal);
        string GetInvalidMessage(Goal goal);
    }

    public class GoalValidator : IGoalValidator
    {
        private readonly OpenLifeContext _context;

        public GoalValidator(OpenLifeContext context)
        {
            _context = context;
        }

        public bool Valid(Goal goal)
        {
            if (string.IsNullOrEmpty(goal.Name) || string.IsNullOrWhiteSpace(goal.Name))
                return false;

            if (!_context.Users.Any(u => u.UserId == goal.UserId))
                return false;

            return goal.StartDate < goal.EndDate;
        }

        public string GetInvalidMessage(Goal goal)
        {
            if (string.IsNullOrEmpty(goal.Name) || string.IsNullOrWhiteSpace(goal.Name))
                return "Name is not valid.";

            if (!_context.Users.Any(u => u.UserId == goal.UserId))
                return $"User with id {goal.UserId} not found.";

            return goal.StartDate > goal.EndDate ? "End date must be after start date." : "";
        }
    }
}
