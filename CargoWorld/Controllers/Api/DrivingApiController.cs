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
    public class DrivingApiController : Controller
    {
        private readonly AppDbContext _context;

        public DrivingApiController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public Car GetDriving(string userMail)
        {
            var user = _context.Users.AsNoTracking().FirstOrDefault(o => o.Email == userMail);
            var car = _context.Cars.AsNoTracking().FirstOrDefault(o => o.IdDriver == user.Id);
            return car;

        }
    }
}