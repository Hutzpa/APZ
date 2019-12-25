using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CargoWorld.Data;
using CargoWorld.Models;

namespace CargoWorld.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargoApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CargoApiController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/CargoApi
        [HttpGet]
        public List<Cargo> GetCargos(string email)
        {
           var cargo =  _context.Cargos.Where(o=>o.Id_Owner.Email == email).AsNoTracking().ToList();
            return cargo;
        }

        // GET: api/CargoApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cargo>> GetCargo(int id)
        {
            var cargo = await _context.Cargos.FindAsync(id);

            if (cargo == null)
            {
                return NotFound();
            }

            return cargo;
        }

        // PUT: api/CargoApi/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCargo(int id, Cargo cargo)
        {
            if (id != cargo.Id_Cargo)
            {
                return BadRequest();
            }

            _context.Entry(cargo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CargoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CargoApi
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Cargo>> PostCargo(Cargo cargo)
        {
            _context.Cargos.Add(cargo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCargo", new { id = cargo.Id_Cargo }, cargo);
        }

        // DELETE: api/CargoApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Cargo>> DeleteCargo(int id)
        {
            var cargo = await _context.Cargos.FindAsync(id);
            if (cargo == null)
            {
                return NotFound();
            }

            _context.Cargos.Remove(cargo);
            await _context.SaveChangesAsync();

            return cargo;
        }

        private bool CargoExists(int id)
        {
            return _context.Cargos.Any(e => e.Id_Cargo == id);
        }
    }
}
