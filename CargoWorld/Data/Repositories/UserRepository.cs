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

        public ApplicationUser Get(string id) => _ctx.Users
            .Include(o=>o.Cargos)
            .Include(o=>o.Cars)
            .Include(o=>o.Groups)
            .FirstOrDefault(o => o.Id == id);



        public void Remove(int id) => _ctx.Remove(Get(id));

        public void Update(ApplicationUser update) => _ctx.Users.Update(update);

        public async Task<bool> SaveChangesAsync() => await _ctx.SaveChangesAsync() != 0 ? true : false;

        public ListViewModel<ApplicationUser> GetAll(string id, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public ListViewModel<ApplicationUser> GetAll(int pageNumber)
        {
            int pageSize = 5;
            int skipAmount = pageSize * (pageNumber - 1);
            int postsCount = _ctx.Users.Count();

            return new ListViewModel<ApplicationUser>
            {
                PageNumber = pageNumber,
                CanNext = postsCount > skipAmount + pageSize,
                List = _ctx.Users
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .Where(o=>o.UserName != "admin")
            };
        }

        public SearchViewModel Search(string keyWord,string idUser)
        {
            var cargo = _ctx.Cargos.Include(o => o.Id_Owner).AsNoTracking().Where(o => o.Id_Owner.Id != idUser);
            var users = _ctx.Users.AsNoTracking().Where(o => o.Id != idUser);

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
            else
            {
                return new SearchViewModel
                {
                    Cargos = new List<Cargo>(),
                    Users = new List<ApplicationUser>()
                };
            }
                return new SearchViewModel
                {
                    Cargos = cargo,
                    Users = users
                };
        }
    }
}
