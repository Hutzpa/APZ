using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CargoWorld.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendImageApiController : Controller
    {
        [HttpGet]
        public string GetImage(string path)
        {
            
            return Convert.ToBase64String(System.IO.File.ReadAllBytes(path)).ToString();
        }
    }
}