using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace open_life_server.V1.Goals.HabitGoals
{
    [Route("api/[controller]")]
    [ApiController]
    public class HabitLogController : ControllerBase
    {
        private readonly GoalsContext _context;

        public HabitLogController(GoalsContext context)
        {
            _context = context;
        }

        // GET: api/HabitLog
        [HttpGet]
        public IEnumerable<HabitLog> Get()
        {
            return _context.HabitLogs.ToList();
        }

        // GET: api/HabitLog/5
        [HttpGet("{id}")]
        public HabitLog Get(int id)
        {
            return _context.HabitLogs.Find(id);
        }

        // POST: api/HabitLog
        [HttpPost]
        public void Post([FromBody] HabitLog value)
        {
            _context.HabitLogs.Add(value);
            _context.SaveChanges();
        }

        // PUT: api/HabitLog/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] HabitLog value)
        {
            var log = _context.HabitLogs.Find(id);

            log.Date = value.Date;
            log.HabitCompleted = value.HabitCompleted;

            _context.HabitLogs.Update(log);
            _context.SaveChanges();
        }

        // DELETE: api/HabitLog/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var log = _context.HabitLogs.Find(id);
            _context.HabitLogs.Remove(log);
            _context.SaveChanges();
        }
    }
}
