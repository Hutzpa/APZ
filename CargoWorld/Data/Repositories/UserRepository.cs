using CargoWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoWorld.Data.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private AppDbContext _ctx;

        public UserRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public void Create(User data) => _ctx._Users.Add(data);

        public User Get(int id) => _ctx._Users.FirstOrDefault(o => o.IdUser == id);

        public IEnumerable<User> GetAll() => _ctx._Users.ToList();

        public IEnumerable<User> GetAll(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id) => _ctx.Remove(Get(id));

        public void Update(User update) => _ctx._Users.Update(update);

        public async Task<bool> SaveChangesAsync() => await _ctx.SaveChangesAsync() != 0 ? true : false;

    }
}
