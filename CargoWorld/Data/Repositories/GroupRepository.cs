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


        public void Remove(int id)
        {
            _ctx.Groups.Remove(Get(id));
        }

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
            };


        }
        public IEnumerable<Group> GetAll(string id)
        {
            ApplicationUser user = _ctx.Users.FirstOrDefault(o => o.Id == id);

            return _ctx.Groups.Where(o => o.IdOwner.Id == user.Id);
        }

        [Obsolete("Переместить метод в CAR REPOSITORY")]
        public List<Car> CreateGroupForCargo(int idCargo)
        {
            //Находим нужный груз
            Cargo cargo = _ctx.Cargos.FirstOrDefault(o => o.Id_Cargo == idCargo);

            //Список машин в групе
            List<Car> carsInThisGroup = new List<Car>();

            //Находим все не занятые машины с подходящим типом грузового отделения и сортируем по убыванию объёма
            var freeCars = _ctx.Cars.AsNoTracking().Where(o => o.IdGroup == null && o.CargoType == cargo.CargoType).OrderBy(or => or.CarryingCapacitySqM).ToList();

            double cargoBulk = cargo.Bulk == 0 ? cargo.Height * cargo.Width * cargo.Length : cargo.Bulk;

            //Если объём груза меньше максимума, то находим наилучшую для него машину
            if (cargoBulk <= freeCars.First().CarryingCapacitySqM)
            {
                for (int i = 0; i < freeCars.Count(); i++)
                {
                    //Если текущий элемент меньше по V чем груз, то запихиваем груз в предыдущую машину(т.к. мы уже проверили первый элемент)
                    if (cargoBulk != freeCars[i].CarryingCapacitySqM)
                        carsInThisGroup.Add(freeCars[i - 1]);

                }
            }
            else
            {
                //груз разделён на столько частей
                byte cargoParts = 1;
                //% груза распиханый по машинам
                double cargoAmount = 0.0;
                //Если груз может быть разделён
                if (cargo.CanBeSepateted)
                {
                    //Проходимся по всем свободным машинам
                    for (int i = 0; i < freeCars.Count(); i++)
                    {
                        //Пытаемся максимально делить груз
                        for (; cargoParts <= 3;)
                        {
                            //Если часть груза оставшаяся после разделения меньше максимального объёма машины, ищем подходящее
                            if (cargoBulk/cargoParts <= freeCars.First().CarryingCapacitySqM)
                            {
                                //находим(или нет) подходящее
                                if (cargoBulk / cargoParts != freeCars[i].CarryingCapacitySqM)
                                {
                                    carsInThisGroup.Add(freeCars[i - 1]);
                                    //считаем, сколько груза мы положили
                                    cargoAmount += 1 / cargoParts;
                                    //считаем оставшийся объём груза
                                    cargoBulk = cargoBulk * (1 - cargoAmount);
                                }
                                else
                                {
                                    //делим груз ещё сильнее
                                    cargoParts++;
                                }
                            }
                        }
                    }
                    //Если по итогу проверки всех машин, груз распихан не полностью, то групу под этот груз сформировать нельзя
                    if (cargoAmount >= 0.98)
                        return carsInThisGroup;
                }
                else
                {
                    return null;
                }
            }
            return null;
        }
    }
}
