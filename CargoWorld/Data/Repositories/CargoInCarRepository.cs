using CargoWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoWorld.Data.Repositories
{
    public class CargoInCarRepository : IRepository<CargoInCar>
    {
        private AppDbContext _ctx;

        public CargoInCarRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }
        public void Create(CargoInCar data) => _ctx.CargoInCars.Add(data);

        public CargoInCar Get(int id) => _ctx.CargoInCars.FirstOrDefault(o => o.Id_Delivery == id);

        public IEnumerable<CargoInCar> GetAll() => _ctx.CargoInCars.ToList();

        public IEnumerable<CargoInCar> GetAll(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CargoInCar> GetAll(string id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id) => _ctx.CargoInCars.Remove(Get(id));

        public async Task<bool> SaveChangesAsync() => await _ctx.SaveChangesAsync() != 0 ? true : false;

        public void Update(CargoInCar update) => _ctx.CargoInCars.Update(update);
    }
}
