using CargoWorld.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoWorld.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<CargoInCar> CargoInCars { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Request> Requests { get; set; }
        override public DbSet<ApplicationUser> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);



            builder.Entity<Car>()
                .HasOne(o => o.IdGroup)
                .WithMany(o => o.Cars)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Car>()
                .HasMany(o => o.CargoInThisCar)
                .WithOne(o => o.Transporter)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<CargoInCar>()
                .HasOne(o => o.Transporter)
                .WithMany(o => o.CargoInThisCar)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Cargo>()
                .HasOne(o => o.Id_Owner)
                .WithMany(o => o.Cargos)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Cargo>()
                .HasMany(o => o.Transfer)
                .WithOne(o => o.Cargo)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ApplicationUser>()
                .HasMany(o => o.Cargos)
                .WithOne(o => o.Id_Owner)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<ApplicationUser>()
                .HasMany(o => o.Cars)
                .WithOne(o => o.IdOwner)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<ApplicationUser>()
                .HasMany(o => o.Groups)
                .WithOne(o => o.IdOwner)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<ApplicationUser>()
                .HasMany(o => o.RequestsToMe)
                .WithOne(o => o.Recip)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
