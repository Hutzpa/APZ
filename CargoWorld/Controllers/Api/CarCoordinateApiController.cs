using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CargoWorld.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CargoWorld.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarCoordinateApiController : ControllerBase
    {
        private AppDbContext _context;

        public CarCoordinateApiController(AppDbContext context)
        {
            _context = context;
        }
        // GET: api/CarCoordinateApi
        [HttpGet]
        public string GetCarCoordinate(int idCar)
        {
            var car = _context.Cars.FirstOrDefault(o => o.IdCar == idCar);
            return car.Coordinates;
        }


        // PUT: api/CarCoordinateApi/5
        [HttpPut]
        public void Put(int idCar,string cord)
        {
            var car = _context.Cars.FirstOrDefault(o => o.IdCar == idCar);
            car.Coordinates = cord;
            _context.SaveChanges();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
