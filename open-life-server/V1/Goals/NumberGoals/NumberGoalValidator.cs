using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace open_life_server.V1.Goals.NumberGoals
{
    public interface INumberGoalValidator
    {
        bool Valid(NumberGoal goal);
        string GetInvalidMessage(NumberGoal goal);
    }

    public class NumberGoalValidator : INumberGoalValidator
    {
        private readonly IGoalValidator _goalValidator;

        public NumberGoalValidator(IGoalValidator goalValidator)
        {
            _goalValidator = goalValidator;
        }

        public bool Valid(NumberGoal goal)
        {
            if (!_goalValidator.Valid(goal))
                return false;

            if (goal.Target <= 0)
                return false;

            if (goal.Logs.Any(log => log.Date < goal.StartDate || log.Date > goal.EndDate))
                return false;

            return !goal.Logs.Any(log => log.Amount <= 0);
        }

        public string GetInvalidMessage(NumberGoal goal)
        {
            var goalInvalidMessage = _goalValidator.GetInvalidMessage(goal);
            if (!string.IsNullOrEmpty(goalInvalidMessage))
                return goalInvalidMessage;

            if (goal.Target <= 0)
                return "Target must be greater than 0.";

            return goal.Logs.Any(log => log.Date < goal.StartDate || log.Date > goal.EndDate) ?
                "Logs must be between the goal start and end date." : "All log amounts must be greater than 0.";
        }
    }
}
