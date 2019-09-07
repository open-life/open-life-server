using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace open_life_server.V1.Goals.NumberGoals
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumberGoalController : ControllerBase
    {
        private readonly OpenLifeContext _context;
        private readonly INumberGoalValidator _validator;

        public NumberGoalController(OpenLifeContext context, INumberGoalValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        // GET: api/NumberGoal
        [HttpGet("{username}")]
        [ProducesResponseType(typeof(IEnumerable<NumberGoal>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get([FromRoute]string username)
        {
            var user = _context.Users.SingleOrDefault(u => u.Username == username);
            if (user == null)
                return NotFound($"User with username {username} not found.");

            return Ok(_context.NumberGoals.Include(g => g.Logs).Where(g => g.UserId == user.UserId).ToList());
        }

        // POST: api/NumberGoal
        [HttpPost]
        [ProducesResponseType(typeof(NumberGoal), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] NumberGoal value)
        {
            if (!_validator.Valid(value))
                return BadRequest(_validator.GetInvalidMessage(value));

            var goal = _context.NumberGoals.Add(value).Entity;
            _context.SaveChanges();

            return Ok(goal);
        }

        // PUT: api/NumberGoal/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(NumberGoal), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put(int id, [FromBody] NumberGoal value)
        {
            if (!_validator.Valid(value))
                return BadRequest(_validator.GetInvalidMessage(value));

            var numberToUpdate = _context.NumberGoals.Include(g => g.Logs).Single(g => g.NumberGoalId == id);
            if (numberToUpdate == null)
                return NotFound();

            numberToUpdate.Name = value.Name;
            numberToUpdate.Target = value.Target;
            foreach (var log in value.Logs)
            {
                if (numberToUpdate.Logs.Any(i => i.NumberLogId == log.NumberLogId))
                {
                    var logToUpdate = numberToUpdate.Logs.Single(i => i.NumberLogId == log.NumberLogId);
                    logToUpdate.Date = log.Date;
                    logToUpdate.Amount = log.Amount;
                    _context.NumberLogs.Update(logToUpdate);
                }
                else
                {
                    _context.NumberLogs.Add(log);
                }
            }

            var goal = _context.NumberGoals.Update(numberToUpdate).Entity;
            _context.SaveChanges();

            return Ok(goal);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var numberToDelete = _context.NumberGoals.Find(id);
            if (numberToDelete == null)
                return NotFound();

            _context.NumberGoals.Remove(numberToDelete);
            _context.SaveChanges();
            return Accepted();
        }
    }
}
