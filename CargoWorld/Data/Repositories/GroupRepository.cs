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
    public class GroupRepository : IRepository<Group>
    {
        private AppDbContext _ctx;

        public GroupRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }
        public void Create(Group data) => _ctx.Groups.Add(data);

        public Group Get(int id) => _ctx.Groups.FirstOrDefault(o => o.IdGroup == id);

        public IEnumerable<Group> GetAll() => _ctx.Groups.ToList();

        public void Remove(int id) => _ctx.Groups.Remove(Get(id));

        public async Task<bool> SaveChangesAsync() => await _ctx.SaveChangesAsync() != 0 ? true : false;

        public void Update(Group update) => _ctx.Update(update);

        /// <summary>
        /// Получение всех машин пользователя что не состоят в групе
        /// </summary>
        public IEnumerable<Car> GetCarsWithoutGroup(string id) => _ctx.Cars.Where(o => o.IdOwner.Id == id && o.IdGroup == null);
        /// <summary>
        /// Выводит все групы конкретного пользователя
        /// </summary>
        public ListViewModel<Group> GetAll(string id, int pageNumber)
        {
            ApplicationUser user = _ctx.Users.FirstOrDefault(o => o.Id == id);

            int pageSize = 5;
            int skipAmount = pageSize * (pageNumber - 1);
            int postsCount = _ctx.Groups.Count();


            return new ListViewModel<Group>
            {
                PageNumber = pageNumber,
                CanNext = postsCount > skipAmount + pageSize,
                List = _ctx.Groups.Where(o => o.IdOwner.Id == user.Id)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToList()
            };


        }




    }
}
