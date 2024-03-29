﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace open_life_server.V1.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly OpenLifeContext _context;
        private readonly IUserValidator _validator;

        public UserController(OpenLifeContext context, IUserValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<User>), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            return Ok(_context.Users.ToList());
        }

        [HttpGet("email/{email}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetWithEmail([FromRoute]string email)
        {
            var user = _context.Users.SingleOrDefault(u => u.Email == email);
            if (user != null)
                return Ok(user);

            return NoContent();
        }

        [HttpGet("username/{username}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetWithUsername([FromRoute]string username)
        {
            var user = _context.Users.SingleOrDefault(u => u.Username == username);
            if (user != null)
                return Ok(user);

            return NoContent();
        }

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

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put(int id, [FromBody] User value)
        {
            if (!_validator.Valid(value))
                return BadRequest(_validator.GetInvalidMessage(value));

            var existingUser = _context.Users.Find(id);

            if (existingUser == null)
                return NotFound();

            existingUser.Name = value.Name;
            existingUser.Username = value.Username;
            existingUser.Email = value.Email;
            existingUser.ImageUrl = value.ImageUrl;

            existingUser = _context.Users.Update(existingUser).Entity;
            _context.SaveChanges();

            return Ok(existingUser);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
                return NotFound();

            _context.Remove(user);
            _context.SaveChanges();

            return Accepted();
        }
    }
}
