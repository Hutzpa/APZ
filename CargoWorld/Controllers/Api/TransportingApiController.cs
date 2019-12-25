using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CargoWorld.Data;
using CargoWorld.Data.Repositories;
using CargoWorld.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CargoWorld.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransportingApiController : Controller
    {
        private readonly AppDbContext _context;


        public TransportingApiController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public List<Cargo> GetImTransporting(string userMail)
        {
            var user = _context.Users.AsNoTracking().FirstOrDefault(o => o.Email == userMail);
            List<Cargo> cargos = new List<Cargo>();
            var cargoInCars = _context.CargoInCars
               .Include(o => o.Cargo)
               .AsNoTracking()
               .Where(o => o.Transporter.IdOwner.Id == user.Id);
            foreach (CargoInCar cr in cargoInCars)
                cargos.Add(_context.Cargos.AsNoTracking().FirstOrDefault(o=>o.Id_Cargo == cr.Cargo.Id_Cargo));

            

            return cargos; 

        }
    }
}