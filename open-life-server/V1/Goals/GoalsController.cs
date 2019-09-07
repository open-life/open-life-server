using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

            var habits = _context.HabitGoals.Include(g => g.Logs).Where(g => g.UserId == user.UserId);
            foreach (var habit in habits)
            {
                result.Add(new GoalOverview { Name = habit.Name, Progress = habit.Logs?.Count(l => l.HabitCompleted) ?? 0, Target = habit.Target, UserId = user.UserId });
            }

            var lists = _context.ListGoals.Include(g => g.Items).Where(g => g.UserId == user.UserId);
            foreach (var list in lists)
            {
                result.Add(new GoalOverview { Name = list.Name, Progress = list.Items?.Count ?? 0, Target = list.Target, UserId = user.UserId });
            }

            var numbers = _context.NumberGoals.Include(g => g.Logs).Where(g => g.UserId == user.UserId);
            foreach (var number in numbers)
            {
                var progress = number.Logs != null && number.Logs.Any() ? number.Logs?.Max(l => l.Amount) : 0;
                result.Add(new GoalOverview { Name = number.Name, Progress = progress.Value, Target = number.Target, UserId = user.UserId });
            }

            return Ok(result);
        }
    }
}
