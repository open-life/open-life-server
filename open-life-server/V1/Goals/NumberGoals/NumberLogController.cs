using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace open_life_server.V1.Goals.NumberGoals
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumberLogController : ControllerBase
    {
        private readonly GoalsContext _context;

        public NumberLogController(GoalsContext context)
        {
            _context = context;
        }

        // GET: api/NumberLog
        [HttpGet]
        public IEnumerable<NumberLog> Get()
        {
            return _context.NumberLogs.ToList();
        }

        // GET: api/NumberLog/5
        [HttpGet("{id}")]
        public NumberLog Get(int id)
        {
            return _context.NumberLogs.Find(id);
        }

        // POST: api/NumberLog
        [HttpPost]
        public void Post([FromBody] NumberLog value)
        {
            _context.NumberLogs.Add(value);
            _context.SaveChanges();
        }

        // PUT: api/NumberLog/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] NumberLog value)
        {
            var log = _context.NumberLogs.Find(id);

            log.Date = value.Date;
            log.Amount = value.Amount;

            _context.NumberLogs.Update(log);
            _context.SaveChanges();
        }

        // DELETE: api/NumberLog/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var log = _context.NumberLogs.Find(id);
            _context.NumberLogs.Remove(log);
            _context.SaveChanges();
        }
    }
}
