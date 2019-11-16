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


        public IEnumerable<Car> GetAll(string id) => _ctx.Cars.Where(o => o.IdOwner.Id == id) ;


        [Obsolete]
        public IEnumerable<Car> GetAll(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id) => _ctx.Cars.Remove(Get(id));

        public void Update(Car update) => _ctx.Cars.Update(update);

        public async Task<bool> SaveChangesAsync() => await _ctx.SaveChangesAsync() != 0 ? true : false;


        public IEnumerable<Car> IAmDriving(int id) => _ctx.Cars.Where(o => o.IdDriver == id);
        /// <summary>
        /// Cars in some special group
        /// </summary>
        /// <param name="idRep"></param>
        /// <returns></returns>
        public IEnumerable<Car> CarsInRep(int idGrp) => _ctx.Cars.Where(o => o.IdGroup.IdGroup == idGrp);




    }
}
