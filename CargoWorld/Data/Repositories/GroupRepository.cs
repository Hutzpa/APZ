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


        #region 
        [Obsolete("Переместить метод в CAR REPOSITORY")]
        public List<Car> CreateGroupForCargo(int idCargo)
        {
            //Находим нужный груз
            Cargo cargo = _ctx.Cargos.AsNoTracking().FirstOrDefault(o => o.Id_Cargo == idCargo);

            //Список машин в групе
            List<Car> carsInThisGroup = new List<Car>();

            // Находим все не занятые машины с подходящим типом грузового отделения и сортируем по убыванию объёма
            var freeCars = _ctx.Cars.AsNoTracking().Where(o => o.IdGroup == null && o.CargoType == cargo.CargoType && o.CarryingCapacitySqM > cargo.Bulk / 3).OrderByDescending(or => or.CarryingCapacitySqM).ToList();

            //почему-то не записывает резульатат, запишу вручную
            double cargoBulk = cargo.Bulk == 0 ? cargo.Height * cargo.Width * cargo.Length : cargo.Bulk;

            //Если объём груза меньше максимума, то находим наилучшую для него машину
            if (cargoBulk <= freeCars.First()?.CarryingCapacitySqM)  //работает как надо
            {
                for (int i = freeCars.Count() - 1; i != 0; i--)
                {
                    //Начинаем с самого маленького, и идём вверх пока не влезет
                    if (cargoBulk <= freeCars[i].CarryingCapacitySqM)
                    {
                        carsInThisGroup.Add(freeCars[i]);
                        //для выхода из цикла
                        i = 1;
                    }

                }
            }
            else
            {
             
                //% груза распиханый по машинам
                int cargoAmount = 0;
                //Если груз может быть разделён
                if (cargo.CanBeSepateted)
                {
                        byte cargoParts = 1;
                    //Проходимся по всем свободным машинам
                    for (int i = 0; i < freeCars.Count();)
                    {
                        //Объём этой машины
                        var bulk = freeCars[i].CarryingCapacitySqM == 0 ? freeCars[i].HeightCargoCompartment * freeCars[i].WidthCargoCompartment * freeCars[i].LengthCargoCompartment : freeCars[i].CarryingCapacitySqM;
                        //груз разделён на столько частей
                        //Пытаемся максимально делить груз
                        for (; cargoParts <= 3;)
                        {
                            //Если часть груза оставшаяся после разделения меньше максимального объёма машины, ищем подходящее,если нет, то увеличиваем количество частей
                            if (cargoBulk / cargoParts < bulk)
                            {
                                //Добавляем машину в потенциальную группу
                                carsInThisGroup.Add(freeCars[i]);
                                //считаем, сколько груза мы положили
                                cargoAmount += (100 / cargoParts);
                                if (cargoAmount >= 98)
                                    return carsInThisGroup;
                                //Считаем оставшеесе свободное место в машине
                                //bulk -= cargoBulk / cargoParts;
                                bulk -= cargoBulk * (100 - cargoAmount) / 100;
                                //считаем оставшийся объём груза
                                cargoBulk = cargoBulk * (100 - cargoAmount) / 100;
                            }
                            else
                            {
                                // делим груз ещё сильнее
                                cargoParts++;
                            }

                        }
                        
                        cargoParts = 2;
                        i++;
                    }
                    // Если по итогу проверки всех машин, груз распихан не полностью, то групу под этот груз сформировать нельзя
                    if (cargoAmount >= 98)
                        return carsInThisGroup;
                    else
                        return null;
                }
                else
                {
                    return null;
                }
            }
            return carsInThisGroup;
        }
        #endregion

    }
}
