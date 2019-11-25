using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CargoWorld.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using CargoWorld.Data.Repositories;
using CargoWorld.Models;
using CargoWorld.Data.FileManager;

namespace CargoWorld
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();





            //services.AddDbContext<AppDbContext>(opt =>
            //     opt.UseSqlServer("Data Source=SQL6007.site4now.net;Initial Catalog=DB_A50403_DbCargo;User Id=DB_A50403_DbCargo_admin;Password=PAssword7;"));


            services.AddDbContext<AppDbContext>(opt =>
                 opt.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=DbCargo;Trusted_Connection=true;MultipleActiveResultSets=true"));


            services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();
           

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Auth/Login/";
            });

            services.AddTransient<IRepository<Car>, CarRepository>();
            services.AddTransient<IRepository<Cargo>, CargoRepository>();
            services.AddTransient<IRepository<Group>, GroupRepository>();
            services.AddTransient<IRepository<CargoInCar>, CargoInCarRepository>();
            services.AddTransient<IRepository<ApplicationUser>, UserRepository>();
            services.AddTransient<IRepository<Request>, RequestRepository>();
            services.AddTransient<IFileManager,FIleManager>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();


            app.UseStatusCodePages();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
