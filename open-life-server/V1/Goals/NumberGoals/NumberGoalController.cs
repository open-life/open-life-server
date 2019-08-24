using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace open_life_server.V1.Goals.NumberGoals
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumberGoalController : ControllerBase
    {
        private readonly GoalsContext _context;

        public NumberGoalController(GoalsContext context)
        {
            _context = context;
        }

        // GET: api/NumberGoal
        [HttpGet]
        public IEnumerable<NumberGoal> Get()
        {
            return _context.NumberGoals.Include(g => g.Logs).ToList();
        }

        // GET: api/NumberGoal/5
        [HttpGet("{id}")]
        public NumberGoal Get(int id)
        {
            return _context.NumberGoals.Include(g => g.Logs).Single(g => g.NumberGoalId == id);
        }

        // POST: api/NumberGoal
        [HttpPost]
        public void Post([FromBody] NumberGoal value)
        {
            _context.NumberGoals.Add(value);
            _context.SaveChanges();
        }

        // PUT: api/NumberGoal/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] NumberGoal value)
        {
            var numberToUpdate = _context.NumberGoals.Find(id);

            numberToUpdate.Name = value.Name;
            numberToUpdate.Target = value.Target;
            numberToUpdate.Logs = value.Logs;

            _context.NumberGoals.Update(numberToUpdate);
            _context.SaveChanges();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var numberToDelete = _context.NumberGoals.Find(id);
            _context.NumberGoals.Remove(numberToDelete);
            _context.SaveChanges();
        }
    }
}
