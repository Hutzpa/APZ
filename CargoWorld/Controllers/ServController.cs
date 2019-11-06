using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CargoWorld.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CargoWorld.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServController : ControllerBase
    {
        // GET: api/Serv
        [HttpGet]
        public string Get()
        {
            var usr = new User
            {
                IdUser = 1,
                Age = 20,
                Sex = "Male",

            };
            return usr.ToString(); 
        }

        // GET: api/Serv/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "";
        }

        // POST: api/Serv
        [HttpPost]
        public void Post([FromBody] string value)
        {
            //BRUH

            //return "TEST TEST TEST";
        }

        // PUT: api/Serv/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
           
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
