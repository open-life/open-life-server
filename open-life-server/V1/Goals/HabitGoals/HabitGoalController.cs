﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace open_life_server.V1.Goals.HabitGoals
{
    [Route("api/[controller]")]
    [ApiController]
    public class HabitGoalController : ControllerBase
    {
        private readonly GoalsContext _context;

        public HabitGoalController(GoalsContext context)
        {
            _context = context;
        }

        // GET: api/HabitGoal
        [HttpGet]
        public IEnumerable<HabitGoal> Get()
        {
            return _context.HabitGoals.ToList();
        }

        // GET: api/HabitGoal/5
        [HttpGet("{id}")]
        public HabitGoal Get(int id)
        {
            return _context.HabitGoals.Find(id);
        }

        // GET: api/HabitGoal/5/logs
        [HttpGet("{id}/logs")]
        public IEnumerable<HabitLog> GetLogs(int id)
        {
            return _context.HabitGoals.Find(id).Logs;
        }

        // POST: api/HabitGoal
        [HttpPost]
        public void Post([FromBody] HabitGoal value)
        {
            _context.HabitGoals.Add(value);
            _context.SaveChanges();
        }

        // PUT: api/HabitGoal/5
        [HttpPut("{id}")]
        public void Put([FromRoute] int id, [FromBody] HabitGoal value)
        {
            var habitToUpdate = _context.HabitGoals.Find(id);

            habitToUpdate.Name = value.Name;
            habitToUpdate.Logs = value.Logs;
            habitToUpdate.Target = value.Target;
            habitToUpdate.StartDate = value.StartDate;

            _context.HabitGoals.Update(habitToUpdate);
            _context.SaveChanges();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var habitToDelete = _context.HabitGoals.Find(id);
            _context.HabitGoals.Remove(habitToDelete);
            _context.SaveChanges();
        }
    }
}
