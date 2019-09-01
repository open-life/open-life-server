using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace open_life_server.V1.Goals.ListGoals
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListGoalController : ControllerBase
    {
        private readonly GoalsContext _context;
        private readonly IListGoalValidator _validator;

        public ListGoalController(GoalsContext context, IListGoalValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        // GET: api/ListGoal
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ListGoal>), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            return Ok(_context.ListGoals.Include(g => g.Items).ToList());
        }

        // GET: api/ListGoal/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ListGoal), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            var goal = _context.ListGoals.Include(g => g.Items).Single(g => g.ListGoalId == id);

            if (goal == null)
                return NotFound();

            return Ok(goal);
        }

        // POST: api/ListGoal
        [HttpPost]
        [ProducesResponseType(typeof(ListGoal), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] ListGoal value)
        {
            if (!_validator.Valid(value))
                return BadRequest(_validator.GetInvalidMessage(value));

            var goal = _context.ListGoals.Add(value).Entity;
            _context.SaveChanges();

            return Ok(goal);
        }

        // PUT: api/ListGoal/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ListGoal), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put(int id, [FromBody] ListGoal value)
        {
            if (!_validator.Valid(value))
                return BadRequest(_validator.GetInvalidMessage(value));

            var listToUpdate = _context.ListGoals.Find(id);
            if (listToUpdate == null)
                return NotFound();

            listToUpdate.Name = value.Name;
            listToUpdate.ListName = value.ListName;
            listToUpdate.Target = value.Target;
            listToUpdate.Items = value.Items;

            var goal = _context.ListGoals.Update(listToUpdate).Entity;
            _context.SaveChanges();

            return Ok(goal);
        }

        // DELETE: api/ListGoal/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ListGoal), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var listToDelete = _context.ListGoals.Find(id);
            if (listToDelete == null)
                return NotFound();

            _context.ListGoals.Remove(listToDelete);
            _context.SaveChanges();

            return Accepted();
        }
    }
}
