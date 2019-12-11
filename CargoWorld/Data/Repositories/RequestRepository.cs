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
    public class RequestRepository : IRepository<Request>
    {
        private AppDbContext _ctx;

        public RequestRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }
        public void Create(Request data) => _ctx.Requests.Add(data);

        public Request Get(int id) => _ctx.Requests.FirstOrDefault(o => o.Id == id);

        public ListViewModel<Request> GetAll(string id, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            var g = Get(id);
            _ctx.Requests.Remove(g);
        }


        public async Task<bool> SaveChangesAsync() => await _ctx.SaveChangesAsync() != 0 ? true : false;

        public void Update(Request update) => _ctx.Update(update);

        /// <summary>
        /// Получение МОЕГО списка запросов
        /// </summary>
        public IEnumerable<Request> SelectRequests(RequestType requestType, string idRecipient)
        {
            var requests = _ctx.Requests.Include(o=>o.Recip)
                .Where(o => o.Recip.Id == idRecipient);

            return requests.Where(o => o.RequestType == (RequestType)requestType);
        }

        public async Task AcceptUserToCompany(int id_Cargo, int id_Group, int id_Request)
        {
            var cargo = _ctx.Cargos.Include(o => o.Id_Owner).FirstOrDefault(c => c.Id_Cargo == id_Cargo);
            var group = _ctx.Groups.Include(o => o.IdOwner)
                .Include(o => o.Cars)
                .ToList().FirstOrDefault(o => o.IdGroup == id_Group);
            var cargoInCar = new CargoInCar
            {
                Transporter = group.Cars.FirstOrDefault(o => o.CargoType == cargo.CargoType),
                AmountOfCarog = 100,
                Cargo = cargo
            };
            _ctx.CargoInCars.Add(cargoInCar);
            Remove(id_Request);
            await SaveChangesAsync();
        }

        public async Task AcceptCompanyToUserAsync(int idCargo, int idGroup, string userId)
        {
            //Принятие предложения компании - пользователю о перевозке грузаЫ

            var cargo = _ctx.Cargos.Include(o => o.Id_Owner).FirstOrDefault(c => c.Id_Cargo == idCargo);

            var group = _ctx.Groups.Include(o => o.IdOwner)
                .Include(o => o.Cars)
                .ToList().FirstOrDefault(o => o.IdGroup == idGroup);

            var cargoInCar = new CargoInCar
            {
                Transporter = group.Cars.FirstOrDefault(o => o.CargoType == cargo.CargoType),
                AmountOfCarog = 100,
                Cargo = cargo
            };

            _ctx.CargoInCars.Add(cargoInCar);

            var toDel = _ctx.Requests.Where(o => o.RequestType == RequestType.CompanyOffersToUser &&
            o.Recip.Id == _ctx.Users.FirstOrDefault(us => us.Id == userId).Id);

            _ctx.Requests.RemoveRange(toDel);

            await SaveChangesAsync();
        }

        public async Task AcceptDrivingAsync(int idCar, string userId)
        {


            var car = _ctx.Cars.FirstOrDefault(o => o.IdCar == idCar);
            var user = _ctx.Users.FirstOrDefault(o => o.Id == userId);

            car.IdDriver = user.Id;
            _ctx.Update(car);


            var toDel = _ctx.Requests.Where(o => o.RequestType == RequestType.DrivingRequest &&
            o.Recip.Id == user.Id);
            _ctx.Requests.RemoveRange(toDel);

            await SaveChangesAsync();

        }

    }
}

