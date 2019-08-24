using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace open_life_server.V1.Goals.ListGoals
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListGoalController : ControllerBase
    {
        private readonly GoalsContext _context;

        public ListGoalController(GoalsContext context)
        {
            _context = context;
        }

        // GET: api/ListGoal
        [HttpGet]
        public IEnumerable<ListGoal> Get()
        {
            return _context.ListGoals.Include(g => g.Items).ToList();
        }

        // GET: api/ListGoal/5
        [HttpGet("{id}")]
        public ListGoal Get(int id)
        {
            return _context.ListGoals.Include(g => g.Items).Single(g => g.ListGoalId == id);
        }

        // POST: api/ListGoal
        [HttpPost]
        public void Post([FromBody] ListGoal value)
        {
            _context.ListGoals.Add(value);
            _context.SaveChanges();
        }

        // PUT: api/ListGoal/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ListGoal value)
        {
            var listToUpdate = _context.ListGoals.Find(id);

            listToUpdate.Name = value.Name;
            listToUpdate.ListName = value.ListName;
            listToUpdate.Target = value.Target;
            listToUpdate.Items = value.Items;

            _context.ListGoals.Update(listToUpdate);
            _context.SaveChanges();
        }

        // DELETE: api/ListGoal/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var listToDelete = _context.ListGoals.Find(id);
            _context.ListGoals.Remove(listToDelete);
            _context.SaveChanges();
        }
    }
}
