﻿using CargoWorld.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoWorld.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
               // Database.EnsureCreated();
        }
       

        public DbSet<Car> Cars { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<CargoInCar> CargoInCars { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<User> _Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=DbCargo;Trusted_Connection=true;MultipleActiveResultSets=true");
        }
    }
}