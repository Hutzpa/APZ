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
        public void Create(Request data) => _ctx.Request.Add(data);

        public Request Get(int id) => _ctx.Request.FirstOrDefault(o => o.Id == id);

        public ListViewModel<Request> GetAll(string id, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id) => _ctx.Request.Remove(Get(id));

        public async Task<bool> SaveChangesAsync() => await _ctx.SaveChangesAsync() != 0 ? true : false;

        public void Update(Request update) => _ctx.Update(update);

        /// <summary>
        /// Получение МОЕГО списка запросов
        /// </summary>
        public IEnumerable<Request> SelectRequests(RequestType requestType, string idRecipient)
        {
            var recip = _ctx.Users.FirstOrDefault(o => o.Id == idRecipient);
            var requests = _ctx.Request.Where(o => o.Recipient.Id == recip.Id);

            return requests.Where(o => o.RequestType == (RequestType)requestType);
        }


        public async Task AcceptCompanyToUserAsync(int idCargo, int idGroup, string userId)
        {

            //.In.FirstOrDefault(o => o.Id_Cargo == idCargo);
            var cargo = _ctx.Cargos.Include(o => o.Id_Owner).ToList().FirstOrDefault(c => c.Id_Cargo == idCargo);
            var group = _ctx.Groups.Include(o=> o.IdOwner).ToList().FirstOrDefault(o => o.IdGroup == idGroup);
            var firstCarInGroup = _ctx.Cars.FirstOrDefault(o => o.IdGroup.IdGroup == group.IdGroup);

            //редачим карго и создаём карго ин кар 

            var cargoInCar = new CargoInCar
            {
                Id_Car = firstCarInGroup,
                AmountOfCarog = 100,
            };

            //Null reference exception
            cargoInCar.Id_Cargo.Add(cargo);
            cargo.Transfer = cargoInCar;

            _ctx.Update(cargo);
            _ctx.Update(cargoInCar);

            var toDel = _ctx.Request.Where(o => o.RequestType == RequestType.CompanyOffersToUser &&
            o.Recipient.Id == _ctx.Users.FirstOrDefault(us => us.Id == userId).Id);

            _ctx.Request.RemoveRange(toDel);

            await SaveChangesAsync();
        }

        public async Task AcceptDrivingAsync(int idCar, string userId)
        {


            var car = _ctx.Cars.FirstOrDefault(o => o.IdCar == idCar);
            var user = _ctx.Users.FirstOrDefault(o => o.Id == userId);

            car.IdDriver = user.Id;
            _ctx.Update(car);


            var toDel = _ctx.Request.Where(o => o.RequestType == RequestType.DrivingRequest &&
            o.Recipient.Id == user.Id);
            _ctx.Request.RemoveRange(toDel);

              await SaveChangesAsync();

        }

    }
}

