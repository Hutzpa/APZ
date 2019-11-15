using CargoWorld.Models;
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

        public ApplicationUser Get(int id) => _ctx.Users.FirstOrDefault(o => o.Id == id.ToString());

        public IEnumerable<ApplicationUser> GetAll() => _ctx.Users.ToList();

        public IEnumerable<ApplicationUser> GetAll(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id) => _ctx.Remove(Get(id));

        public void Update(ApplicationUser update) => _ctx.Users.Update(update);

        public async Task<bool> SaveChangesAsync() => await _ctx.SaveChangesAsync() != 0 ? true : false;

        public IEnumerable<ApplicationUser> GetAll(string id)
        {
            throw new NotImplementedException();
        }
    }
}
