using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CargoWorld.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string blabla { get; set; }

        
    }
}
