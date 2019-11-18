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
    public class UserRepository : IRepository<ApplicationUser>
    {
        private AppDbContext _ctx;

        public UserRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public void Create(ApplicationUser data) => _ctx.Users.Add(data);

        public ApplicationUser Get(int id) => throw new NotImplementedException();

        public ApplicationUser Get(string id) => _ctx.Users.FirstOrDefault(o => o.Id == id);



        public void Remove(int id) => _ctx.Remove(Get(id));

        public void Update(ApplicationUser update) => _ctx.Users.Update(update);

        public async Task<bool> SaveChangesAsync() => await _ctx.SaveChangesAsync() != 0 ? true : false;

        public ListViewModel<ApplicationUser> GetAll(string id, int pageNumber)
        {
            throw new NotImplementedException();
        }


        public SearchViewModel Search(string keyWord)
        {
            var cargo = _ctx.Cargos.AsNoTracking();
            var users = _ctx.Users.AsNoTracking();

            if (!String.IsNullOrEmpty(keyWord))
            {
                cargo = cargo.Where(o =>
                EF.Functions.Like(o.CargoName, $"%{keyWord}%") ||
                EF.Functions.Like(o.CargoType, $"%{keyWord}%") ||
                EF.Functions.Like(o.DeparturePoint, $"%{keyWord}%") ||
                EF.Functions.Like(o.DestinationPoint, $"%{keyWord}%"));

                users = users.Where(u =>
                EF.Functions.Like(u.Name, $"%{keyWord}%") ||
                EF.Functions.Like(u.Surname, $"%{keyWord}%")||
                EF.Functions.Like(u.Email, $"%{keyWord}%"));


            }
                return new SearchViewModel
                {
                    Cargos = cargo,
                    Users = users
                };
        }
    }
}
