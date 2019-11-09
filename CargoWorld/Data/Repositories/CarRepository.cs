using CargoWorld.Models;
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


        public Car Get(int id) => _ctx.Cars.FirstOrDefault(o => o.IdCar == id);


        public IEnumerable<Car> GetAll() => _ctx.Cars.ToList();


        [Obsolete]
        public IEnumerable<Car> GetAll(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id) => _ctx.Cars.Remove(Get(id));

        public void Update(Car update) => _ctx.Cars.Update(update);

        public async Task<bool> SaveChangesAsync() => await _ctx.SaveChangesAsync() != 0 ? true : false;

        [Obsolete("Почитай про ")]
        public IEnumerable<Car> IAmDriving(int MyId)
        {
            var res = from t in _ctx.Cars
                      where t.IdDriver == MyId
                      select t;
            return res;
        }



    }
}
