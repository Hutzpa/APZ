using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CargoWorld.Data;
using CargoWorld.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CargoWorld.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetGroupApiController : Controller
    {
        private readonly AppDbContext _context;

        public GetGroupApiController(AppDbContext context)
        {
            _context = context;
        }

        //[HttpGet]
        //public IEnumerable<Car> CarsInRep(int idGrp) => _context.Cars
        //    .Where(o => o.IdGroup.IdGroup == idGrp).AsNoTracking();

        [HttpGet]
        public List<Car> GetCars(int id)
        {
            var cars = _context.Cars.AsNoTracking().Where(o => o.IdGroup.IdGroup == id).ToList();
            //List<Car> cars = car.Cars;
            return cars;

        }
    }
}