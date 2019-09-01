using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace open_life_server.V1.Goals.ListGoals
{
    public interface IListGoalValidator
    {
        bool Valid(ListGoal goal);
        string GetInvalidMessage(ListGoal goal);
    }

    public class ListGoalValidator : IListGoalValidator
    {
        private readonly IGoalValidator _goalValidator;

        public ListGoalValidator(IGoalValidator goalValidator)
        {
            _goalValidator = goalValidator;
        }

        public bool Valid(ListGoal goal)
        {
            if (!_goalValidator.Valid(goal))
                return false;

            if (string.IsNullOrEmpty(goal.ListName) || string.IsNullOrWhiteSpace(goal.ListName))
                return false;

            if (goal.Target <= 0)
                return false;

            return !goal.Items.Any(item => string.IsNullOrEmpty(item.Name) || string.IsNullOrWhiteSpace(item.Name));
        }

        public string GetInvalidMessage(ListGoal goal)
        {
            var goalInvalidMessage = _goalValidator.GetInvalidMessage(goal);
            if (!string.IsNullOrEmpty(goalInvalidMessage))
                return goalInvalidMessage;

            if (string.IsNullOrEmpty(goal.ListName) || string.IsNullOrWhiteSpace(goal.ListName))
                return "List must have a valid name.";

            return goal.Target <= 0 ? "Target must be greater than 0." : "All list items must have a name.";
        }
    }
}
