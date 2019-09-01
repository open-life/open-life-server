using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace open_life_server.V1.Goals.HabitGoals
{
    [Route("api/[controller]")]
    [ApiController]
    public class HabitGoalController : ControllerBase
    {
        private readonly OpenLifeContext _context;
        private readonly IHabitGoalValidator _validator;

        public HabitGoalController(OpenLifeContext context, IHabitGoalValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        [HttpGet("{username}")]
        [ProducesResponseType(typeof(IEnumerable<HabitGoal>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get([FromRoute]string username)
        {
            var user = _context.Users.SingleOrDefault(u => u.Username == username);
            if (user == null)
                return NotFound($"User with username {username} not found.");

            return Ok(_context.HabitGoals.Include(h => h.Logs).Where(g => g.UserId == user.UserId).ToList());
        }

        [HttpPost]
        [ProducesResponseType(typeof(HabitGoal), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] HabitGoal value)
        {
            if (!_validator.Valid(value))
                return BadRequest(_validator.GetInvalidMessage(value));

            var goal = _context.HabitGoals.Add(value).Entity;
            _context.SaveChanges();

            return Ok(goal);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(HabitGoal), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put([FromRoute] int id, [FromBody] HabitGoal value)
        {
            if (!_validator.Valid(value))
                return BadRequest(_validator.GetInvalidMessage(value));

            var habitToUpdate = _context.HabitGoals.Find(id);

            if (habitToUpdate == null)
                return NotFound();

            habitToUpdate.Name = value.Name;
            habitToUpdate.Logs = value.Logs;
            habitToUpdate.Target = value.Target;
            habitToUpdate.StartDate = value.StartDate;

            var goal = _context.HabitGoals.Update(habitToUpdate).Entity;
            _context.SaveChanges();
            return Ok(goal);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var habitToDelete = _context.HabitGoals.Find(id);

            if (habitToDelete == null)
                return NotFound();

            _context.HabitGoals.Remove(habitToDelete);
            _context.SaveChanges();
            return Accepted();
        }
    }
}
