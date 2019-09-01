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
        private readonly OpenLifeContext _context;

        public GoalsController(OpenLifeContext context)
        {
            _context = context;
        }

        // GET: api/Goals
        [HttpGet("{username}")]
        [ProducesResponseType(typeof(IEnumerable<GoalOverview>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get([FromRoute]string username)
        {
            var user = _context.Users.SingleOrDefault(u => u.Username == username);
            if (user == null)
                return NotFound($"User with username {username} not found.");

            var result = new List<GoalOverview>();

            var habits = _context.HabitGoals.Where(g => g.UserId == user.UserId);
            foreach (var habit in habits)
            {
                result.Add(new GoalOverview { Name = habit.Name, Progress = $"{(habit.Logs?.Count(l => l.HabitCompleted) ?? 0 / habit.Target) * 100}%" });
            }

            var lists = _context.ListGoals.Where(g => g.UserId == user.UserId);
            foreach (var list in lists)
            {
                result.Add(new GoalOverview { Name = list.Name, Progress = $"{list.Items?.Count ?? 0}/{list.Target}" });
            }

            var numbers = _context.NumberGoals.Where(g => g.UserId == user.UserId);
            foreach (var number in numbers)
            {
                result.Add(new GoalOverview { Name = number.Name, Progress = $"{number.Logs?.Max(l => l.Amount) ?? 0}" });
            }

            return Ok(result);
        }
    }
}
