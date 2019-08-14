using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace open_life_server.V1.Goals.HabitGoals
{
    [Route("api/[controller]")]
    [ApiController]
    public class HabitGoalController : ControllerBase
    {
        // GET: api/HabitGoal
        [HttpGet]
        public IEnumerable<HabitGoal> Get()
        {
            using (var db = new GoalsContext())
            {
                return db.HabitGoals.ToList();
            }
        }

        // GET: api/HabitGoal/5
        [HttpGet("{id}")]
        public HabitGoal Get(int id)
        {
            using (var db = new GoalsContext())
            {
                return db.HabitGoals.FirstOrDefault(g => g.HabitGoalId == id);
            }
        }

        // POST: api/HabitGoal
        [HttpPost]
        public void Post([FromBody] HabitGoal value)
        {
            using (var db = new GoalsContext())
            {
                db.HabitGoals.Add(value);
                db.SaveChanges();
            }
        }

        // PUT: api/HabitGoal/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
