using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CargoWorld.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CargoWorld.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthApiController : ControllerBase
    {
        private SignInManager<ApplicationUser> _signInManager;
        private UserManager<ApplicationUser> _userManager;
        public AuthApiController(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }


        //// GET: api/AuthApi
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}


        public class LoginView
        {
            public bool IsSuccess { get; set; }
            public string UserId { get; set; }
        }

        // GET: api/AuthApi/5
        //[HttpGet("{id}", Name = "Get")]
        [HttpGet]
        public async Task<bool> GetAsync(string login, string password)
        {
            var res = await _signInManager.PasswordSignInAsync(login, password, false, false);
            if (res.Succeeded)
            {
                return true;
            }
            return false;
        }

      

        // POST: api/AuthApi
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/AuthApi/5
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
