using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace open_life_server.V1.Goals
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoalsController : ControllerBase
    {
        private readonly GoalsContext _context;

        public GoalsController(GoalsContext context)
        {
            _context = context;
        }

        // GET: api/Goals
        [HttpGet]
        public IEnumerable<GoalOverview> Get()
        {
            var result = new List<GoalOverview>();

            var habits = _context.HabitGoals.ToList();
            foreach (var habit in habits)
            {
                result.Add(new GoalOverview{Name = habit.Name, Progress = $"{(habit.Logs?.Count(l => l.HabitCompleted) ?? 0 / habit.Target) * 100}%"});
            }

            var lists = _context.ListGoals.ToList();
            foreach (var list in lists)
            {
                result.Add(new GoalOverview{Name = list.Name, Progress = $"{list.Items?.Count ?? 0}/{list.Target}"});
            }

            var numbers = _context.NumberGoals.ToList();
            foreach (var number in numbers)
            {
                result.Add(new GoalOverview{Name = number.Name, Progress = $"{number.Logs?.Max(l => l.Amount) ?? 0}"});
            }

            return result;
        }
    }
}
