using CargoWorld.ViewModels;
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
        ListViewModel<T> GetAll(string id,int pageNumber);  

        void Create(T data);

        void Update(T update);

        void Remove(int id);

        Task<bool> SaveChangesAsync();
    }
}
