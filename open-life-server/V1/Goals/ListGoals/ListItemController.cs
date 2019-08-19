using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace open_life_server.V1.Goals.ListGoals
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListItemController : ControllerBase
    {
        private readonly GoalsContext _context;

        public ListItemController(GoalsContext context)
        {
            _context = context;
        }

        // GET: api/ListItem
        [HttpGet]
        public IEnumerable<ListItem> Get()
        {
            return _context.ListItems.ToList();
        }

        // GET: api/ListItem/5
        [HttpGet("{id}")]
        public ListItem Get(int id)
        {
            return _context.ListItems.Find(id);
        }

        // POST: api/ListItem
        [HttpPost]
        public void Post([FromBody] ListItem value)
        {
            _context.ListItems.Add(value);
            _context.SaveChanges();
        }

        // PUT: api/ListItem/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ListItem value)
        {
            var item = _context.ListItems.Find(id);

            item.Name = value.Name;
            item.Progress = value.Progress;

            _context.ListItems.Update(item);
            _context.SaveChanges();
        }

        // DELETE: api/ListItem/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var item = _context.ListItems.Find(id);
            _context.ListItems.Remove(item);
            _context.SaveChanges();
        }
    }
}
