using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CargoWorld.Data;
using CargoWorld.Models;
using CargoWorld.Data.Repositories;
using Newtonsoft.Json;

namespace CargoWorld.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServController : ControllerBase
    {
        private readonly AppDbContext _context;
       // private readonly UserRepository _users;

        public ServController(AppDbContext context)
        {
            _context = context;

        }

        // GET: api/Serv
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context._Users.ToListAsync();
        }

        

        // GET: api/Serv/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            Models.User user = new User
            {
                IdUser = 1,
                Age = 50,
                Sex = "Male",
                Name = "Name",
                Surname = "Surname"

            };
            return user;
        }

        // PUT: api/Serv/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut]
        public async Task<IActionResult> PutUser(string user)
        {

            _context._Users.Add(JsonConvert.DeserializeObject<User>(user));
            _context.SaveChanges();

            return NoContent();
        }

        // POST: api/Serv
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context._Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.IdUser }, user);
        }

        // DELETE: api/Serv/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _context._Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context._Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(int id)
        {
            return _context._Users.Any(e => e.IdUser == id);
        }
    }
}
