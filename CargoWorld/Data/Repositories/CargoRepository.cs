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
    public class CargoRepository : IRepository<Cargo>
    {
        private AppDbContext _ctx;

        public CargoRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }
        public void Create(Cargo data) => _ctx.Cargos.Add(data);

        public Cargo Get(int id)
        {
            //var ownerId = _ctx.Cargos.FirstOrDefault(o => o.Id_Cargo == id).Id_Owner;
            var ownerId = _ctx.Cargos.Include(o => o.Id_Owner).ToList().FirstOrDefault(o => o.Id_Cargo == id);


            return ownerId;
        }





        /// <summary>
        /// Выводит все грузы конкретного пользователя
        /// </summary>
        /// <param name="id">номер пользователя</param>
        /// <returns></returns>
        public ListViewModel<Cargo> GetAll(string id, int pageNumber)
        {
            ApplicationUser user = _ctx.Users.FirstOrDefault(o => o.Id == id);

            int pageSize = 5;
            int skipAmount = pageSize * (pageNumber - 1);
            int postsCount = _ctx.Cargos.Count();

            return new ListViewModel<Cargo>
            {
                PageNumber = pageNumber,
                CanNext = postsCount > skipAmount + pageSize,
                List = _ctx.Cargos.Where(o => o.Id_Owner.Id == user.Id)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)

            };
        }

        public void Remove(int id) => _ctx.Cargos.Remove(Get(id));
        public async Task<bool> SaveChangesAsync() => await _ctx.SaveChangesAsync() != 0 ? true : false;
        public void Update(Cargo update) => _ctx.Cargos.Update(update);



        #region Найти оптимальный груз для группы
        public IEnumerable<Cargo> SearchOptimalCargoForGroup(int groupId)
        {

            //Искомая група и машины в ней и грузы в этих машинах
            var group = _ctx.Groups.Include(o => o.Cars).Include(o=>o.IdOwner).FirstOrDefault(g => g.IdGroup == groupId);

            //Грузы, рекомендуемые для этой групы
            List<Cargo> cargosForThisGroup = new List<Cargo>();



            //все грузы
            foreach (var c in _ctx.Cargos.AsNoTracking())
            {
                //Получаем свободное пространство во всех грузовиках этой группы для подходящего типа груза
                double groupFreeSpace = 0.0;
                foreach (Car car in group.Cars.Where(car => car.CargoType == c.CargoType))
                {
                    var cagroInCar = _ctx.CargoInCars.Include(o => o.Transporter).Where(cg => cg.Transporter == car);
                    //Весь объём грузового отделения машины
                    double totalBulk = (car.HeightCargoCompartment * car.WidthCargoCompartment * car.LengthCargoCompartment) == 0 ? car.CarryingCapacitySqM : (car.HeightCargoCompartment * car.WidthCargoCompartment * car.LengthCargoCompartment);
                    //Занятый объём грузового отделения
                    double bussyBulk = 0.0;
                    //Если в машине есть груз, считаем занятый объём
                    if (cagroInCar.Count() != 0)
                    {
                        foreach (CargoInCar cargo in cagroInCar)
                            //Прибавляем занятый каждым грузом объём, если он не указан как параметр, указываем
                            bussyBulk += (cargo.Cargo.Height * cargo.Cargo.Width * cargo.Cargo.Length) == 0
                                ? cargo.Cargo.Bulk
                                : (cargo.Cargo.Height * cargo.Cargo.Width * cargo.Cargo.Length);
                    }
                    //считаем свободное пространство автомобиля и прибавляем к свободному пространству подходящего типа груза в групе
                    groupFreeSpace += totalBulk - bussyBulk;
                }
                //На сколько частей делят груз в данный момент
                byte countOfParts = 1;
                //Сформированность груза на данный момент
                int percentOfCargoPlacing = 0;
                //Объём этого груза
                double cargoBulk = c.Bulk == 0 ? (c.Height * c.Width * c.Length) / countOfParts : c.Bulk / countOfParts;

                if (groupFreeSpace > cargoBulk)
                {
                    //Цикл для всех машин с типом грузового отделения подходящим под тип груза
                    foreach (Car car in group.Cars.Where(car => car.CargoType == c.CargoType))
                    {

                        //получаем все грузы в этой машине
                        var cagroInCar = _ctx.CargoInCars.Include(o => o.Transporter).Where(cg => cg.Transporter == car);
                        //Объём грузового отделения
                        double totalBulk = (car.HeightCargoCompartment * car.WidthCargoCompartment * car.LengthCargoCompartment) == 0 ? car.CarryingCapacitySqM : (car.HeightCargoCompartment * car.WidthCargoCompartment * car.LengthCargoCompartment);
                        //Счётчик занятого объёма в грузовом отделении этой машины
                        double bussyBulk = 0.0;


                        foreach (CargoInCar cargo in cagroInCar)
                        {
                            //Считаем занятный всеми грузами объём грузового отделения 
                            bussyBulk += cargo.Cargo.Bulk == 0
                                ? cargo.Cargo.Height * cargo.Cargo.Width * cargo.Cargo.Length
                                : cargo.Cargo.Bulk;
                        }

                        //Если свободного пространства в грузовом отделении хватает чтоб запихнуть туда груз целиком, то запихиваем
                        if (bussyBulk + cargoBulk <= totalBulk) //работает, добавляет
                            cargosForThisGroup.Add(c);
                        else //а если нет.....
                        {
                            //может ли быть груз разделён? нет - выходим, переходим к другой машине
                            if (c.CanBeSepateted)
                            {
                                //Степень разделения груза
                                for (; countOfParts <= 3;)
                                {
                                    //Если свободный объём в этой машине, больше чем объём груза, то мы его запихиваем, и записываем 
                                    if (bussyBulk + cargoBulk / countOfParts < totalBulk)
                                    {
                                        //% 
                                        percentOfCargoPlacing += (100 / countOfParts);
                                        //делим груз и получаем оставшийся объём
                                        cargoBulk = cargoBulk / countOfParts;
                                        //прибавляем к занятому объёму грузового отделения объём части груза
                                        bussyBulk += cargoBulk;

                                    }
                                    else //или же, проверяем,не влезет ли в машину груз делённый ещё сильнее
                                        countOfParts++;
                                }
                            }
                        }

                    }
                }
                //Если по истечении всех проверок, груз не сформирован полностью, то его не добавляют и переходят к следующему
                if (percentOfCargoPlacing >= 98)
                    cargosForThisGroup.Add(c);
            }
            // Список грузов рекомендованный для этой групы
            return cargosForThisGroup;
        }
        private bool CanIPlaceCargoHere(double bussyBulkInThisCar, double totalBulkInThisCar, double cargoBulk)
        {
            return bussyBulkInThisCar + cargoBulk > totalBulkInThisCar;
        }

        #endregion
    }
}
