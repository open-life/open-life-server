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
        private readonly GoalsContext _context;
        private readonly HabitGoalValidator _validator;

        public HabitGoalController(GoalsContext context, HabitGoalValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        // GET: api/HabitGoal
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<HabitGoal>), StatusCodes.Status200OK)]
        public IEnumerable<HabitGoal> Get()
        {
            return _context.HabitGoals.Include(h => h.Logs).ToList();
        }

        // GET: api/HabitGoal/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(HabitGoal), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            var goal = _context.HabitGoals.Include(h => h.Logs).Single(h => h.HabitGoalId == id);

            if (goal == null)
                return NotFound();

            return Ok(goal);
        }

        // POST: api/HabitGoal
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

        // PUT: api/HabitGoal/5
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

        // DELETE: api/ApiWithActions/5
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
