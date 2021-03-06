﻿using CargoWorld.Models;
using CargoWorld.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoWorld.Data.Repositories
{
    public class CarRepository : IRepository<Car>
    {
        private AppDbContext _ctx;

        public CarRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public void Create(Car data) => _ctx.Cars.Add(data);


        public Car Get(int id) => _ctx.Cars.AsNoTracking().Include(o=>o.IdGroup).FirstOrDefault(o => o.IdCar == id);
          public Car GetTracking(int id) => _ctx.Cars.Include(o=>o.IdGroup).FirstOrDefault(o => o.IdCar == id);

        /// <summary>
        /// Все машины конкретного пользователя
        /// </summary>
        /// <param name="id">Номер пользователя</param>
        /// <returns></returns>
        public ListViewModel<Car> GetAll(string id, int pageNumber)
        {
           

            int pageSize = 5;
            int skipAmount = pageSize * (pageNumber - 1);
            int postsCount = _ctx.Cars
                .Include(o=>o.IdOwner)
                .Where(o=>o.IdOwner.Id == id)
                .Count();

            return new ListViewModel<Car>
            {
                PageNumber = pageNumber,
                CanNext = postsCount > skipAmount + pageSize,
                List = _ctx.Cars
                .Include(o=>o.IdOwner)
                .Where(o => o.IdOwner.Id == id)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
            };

        }

        public IEnumerable<Car> GetAll(string id)
        {
            ApplicationUser user = _ctx.Users.FirstOrDefault(o => o.Id == id);
            var cars = _ctx.Cars.Where(o => o.IdOwner.Id == user.Id);
            return cars;
        }

        public void Remove(int id) => _ctx.Cars.Remove(Get(id));

        public void Update(Car update)
        {
            _ctx.Cars.Update(update);
        }


        public async Task<bool> SaveChangesAsync() => await _ctx.SaveChangesAsync() != 0 ? true : false;


        public Car IAmDriving(string id) => _ctx.Cars.FirstOrDefault(o => o.IdDriver == id);

        public async Task RefuseDrivingAsync(int idCar)
        {
            Car car = _ctx.Cars.FirstOrDefault(o => o.IdCar == idCar);
            car.IdDriver = null;
            await SaveChangesAsync();
        }

        /// <summary>
        /// Cars in some special group
        /// </summary>
        /// <param name="idRep"></param>
        /// <returns></returns>
        public IEnumerable<Car> CarsInRep(int idGrp) => _ctx.Cars.Include(o=>o.IdGroup)
            .Where(o => o.IdGroup.IdGroup == idGrp);

    }
}
