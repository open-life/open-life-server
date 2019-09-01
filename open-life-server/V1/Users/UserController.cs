using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace open_life_server.V1.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UsersContext _context;
        private readonly IUserValidator _validator;

        public UserController(UsersContext context, IUserValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        // GET: api/User
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<User>), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            return Ok(_context.Users.ToList());
        }

        // GET: api/User/pchaffee@gmail.com
        [HttpGet("{email}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Get(string email)
        {
            var user = _context.Users.SingleOrDefault(u => u.Email == email);
            if (user != null)
                return Ok(user);

            return NoContent();
        }

        // POST: api/User
        [HttpPost]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] User value)
        {
            if (!_validator.Valid(value))
                return BadRequest(_validator.GetInvalidMessage(value));

            var user = _context.Users.Add(value).Entity;
            _context.SaveChanges();
            return Ok(user);
        }

        // PUT: api/User/5
        [HttpPut("{email}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put(string email, [FromBody] User value)
        {
            if (!_validator.Valid(value))
                return BadRequest(_validator.GetInvalidMessage(value));

            var existingUser = _context.Users.SingleOrDefault(u => u.Email == email);

            if (existingUser == null)
                return NotFound();

            existingUser.Name = value.Name;
            existingUser.Email = value.Email;
            existingUser.ImageUrl = value.ImageUrl;

            existingUser = _context.Users.Update(existingUser).Entity;
            _context.SaveChanges();
            return Ok(existingUser);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{email}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(string email)
        {
            var user = _context.Users.SingleOrDefault(u => u.Email == email);
            if (user == null)
                return NotFound();

            _context.Remove(user);
            return Accepted();
        }
    }
}
