using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CargoWorld.Data;
using CargoWorld.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CargoWorld
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            var scope = host.Services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            context.Database.EnsureCreated();

            var adminRole = new IdentityRole("Admin");
            if (!context.Roles.Any())
            {
                roleMgr.CreateAsync(adminRole).GetAwaiter().GetResult();
            }

            if (!context.Users.Any(u => u.UserName == "admin"))
            {
                

                var adminUser = new ApplicationUser
                {
                    UserName = "admin",
                    Email = "ilia.azek@gmail.com"
                };
                var result = userMgr.CreateAsync(adminUser,"Password1_").GetAwaiter().GetResult();

                userMgr.AddToRoleAsync(adminUser,adminRole.Name).GetAwaiter().GetResult();
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
