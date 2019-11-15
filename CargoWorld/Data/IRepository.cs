using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoWorld.Data
{
    public interface IRepository<T> 
    {
        T Get(int id);
        IEnumerable<T> GetAll(string id);  
        IEnumerable<T> GetAll(int id);

        void Create(T data);

        void Update(T update);

        void Remove(int id);

        Task<bool> SaveChangesAsync();
    }
}
