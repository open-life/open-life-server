using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace open_life_server.V1.Goals.HabitGoals
{
    public interface IHabitGoalValidator
    {
        bool Valid(HabitGoal goal);
        string GetInvalidMessage(HabitGoal goal);
    }

    public class HabitGoalValidator : IHabitGoalValidator
    {
        private readonly IGoalValidator _goalValidator;

        public HabitGoalValidator(IGoalValidator goalValidator)
        {
            _goalValidator = goalValidator;
        }

        public bool Valid(HabitGoal goal)
        {
            if (!_goalValidator.Valid(goal))
                return false;

            return goal.Target > 0 && goal.Logs.All(log => log.Date >= goal.StartDate && log.Date <= goal.EndDate);
        }

        public string GetInvalidMessage(HabitGoal goal)
        {
            var goalInvalidMessage = _goalValidator.GetInvalidMessage(goal);
            if (!string.IsNullOrEmpty(goalInvalidMessage))
                return goalInvalidMessage;

            return goal.Target <= 0 ? "Target must be greater than 0." : "Logs must be between the goal start and end date.";
        }
    }
}
