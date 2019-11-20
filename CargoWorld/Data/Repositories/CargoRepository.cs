using CargoWorld.Models;
using CargoWorld.ViewModels;
using Microsoft.EntityFrameworkCore;
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

        public CargoRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }
        public void Create(Cargo data) => _ctx.Cargos.Add(data);
         
        public Cargo Get(int id)
        {
            //var ownerId = _ctx.Cargos.FirstOrDefault(o => o.Id_Cargo == id).Id_Owner;
            var ownerId = _ctx.Cargos.Include(o => o.Id_Owner).ToList().FirstOrDefault(o => o.Id_Cargo == id);


            return ownerId;
        }





        /// <summary>
        /// Выводит все грузы конкретного пользователя
        /// </summary>
        /// <param name="id">номер пользователя</param>
        /// <returns></returns>
        public ListViewModel<Cargo> GetAll(string id, int pageNumber)
        {
            ApplicationUser user = _ctx.Users.FirstOrDefault(o => o.Id == id);

            int pageSize = 5;
            int skipAmount = pageSize * (pageNumber - 1);
            int postsCount = _ctx.Cargos.Count();

            return new ListViewModel<Cargo>
            {
                PageNumber = pageNumber,
                CanNext = postsCount > skipAmount + pageSize,
                List = _ctx.Cargos.Where(o => o.Id_Owner.Id == user.Id)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)

            };
        }

        public void Remove(int id) => _ctx.Cargos.Remove(Get(id));
        public async Task<bool> SaveChangesAsync() => await _ctx.SaveChangesAsync() != 0 ? true : false;
        public void Update(Cargo update) => _ctx.Cargos.Update(update);
    }
}
