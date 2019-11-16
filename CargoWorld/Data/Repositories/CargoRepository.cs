using CargoWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoWorld.Data.Repositories
{
    public class CargoRepository : IRepository<Cargo>
    {
        private AppDbContext _ctx;

        public CargoRepository(AppDbContext ctx )
        {
            _ctx = ctx;
        }
        public void Create(Cargo data) => _ctx.Cargos.Add(data);

        public Cargo Get(int id) => _ctx.Cargos.FirstOrDefault(o => o.Id_Cargo == id);

        public IEnumerable<Cargo> GetAll() => _ctx.Cargos.ToList();

        public IEnumerable<Cargo> GetAll(int id) => throw new NotImplementedException();

        [Obsolete("Выводит лишь один груз")]
        public IEnumerable<Cargo> GetAll(string id) => _ctx.Cargos.Where(o => o.Id_Owner.Id == id);
        public void Remove(int id) => _ctx.Cargos.Remove(Get(id));
        public async Task<bool> SaveChangesAsync() => await _ctx.SaveChangesAsync() != 0 ? true : false;
        public void Update(Cargo update) => _ctx.Cargos.Update(update);
    }
}
