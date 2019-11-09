using CargoWorld.Models;
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

        public IEnumerable<Group> GetAll() => throw new NotImplementedException();

        public IEnumerable<Group> GetAll(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id) => _ctx.Groups.Remove(Get(id));

        public async Task<bool> SaveChangesAsync() => await _ctx.SaveChangesAsync() != 0 ? true : false;

        public void Update(Group update) => _ctx.Update(update);
    }
}
