using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CargoWorld.Data;
using CargoWorld.Models;
using Newtonsoft.Json;

namespace CargoWorld.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsAPIController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CarsAPIController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Car> GetCars(string email)
        {
            var car = _context.Cars.Where(o => o.IdOwner.Email == email).AsNoTracking().ToList();
            return car;
            
        }

       
        //[HttpGet("{idGroup}")]
        //public List<Car> GetCars(int idGroup)
        //{
        //    var car = _context.Cars.Where(o => o.IdGroup.IdGroup == idGroup).AsNoTracking().ToList();
        //    return car;

        //}

        //// PUT: api/CarsAPI/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for
        //// more details see https://aka.ms/RazorPagesCRUD.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutCar(int id, Car car)
        //{
        //    if (id != car.IdCar)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(car).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CarExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/CarsAPI
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for
        //// more details see https://aka.ms/RazorPagesCRUD.
        //[HttpPost]
        //public async Task<ActionResult<Car>> PostCar(Car car)
        //{
        //    _context.Cars.Add(car);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetCar", new { id = car.IdCar }, car);
        //}

        //// DELETE: api/CarsAPI/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Car>> DeleteCar(int id)
        //{
        //    var car = await _context.Cars.FindAsync(id);
        //    if (car == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Cars.Remove(car);
        //    await _context.SaveChangesAsync();

        //    return car;
        //}

        //private bool CarExists(int id)
        //{
        //    return _context.Cars.Any(e => e.IdCar == id);
        //}
    }
}
