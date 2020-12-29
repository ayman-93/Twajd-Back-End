using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Twajd_Back_End.Core.Helper;
using Twajd_Back_End.Core.Models;
using Twajd_Back_End.Core.Repositories;
using Twajd_Back_End.Core.Services;

namespace Twajd_Back_End.Business.Services
{
    public class LocationsService : ILocationsService
    {
        private readonly IUnitOfWork _unitOfWork;
        public LocationsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Location>> Get(Guid managerId)
        {
            return await _unitOfWork.LocationRepository.Get(filter: wh => wh.manager.Id == managerId);
        }

        public async Task<Location> GetById(Guid locationId)
        {
            return await _unitOfWork.LocationRepository.GetById(locationId);
        }

        public void AddLocation(Location location)
        {
            _unitOfWork.LocationRepository.Insert(location);
            _unitOfWork.Commit();
        }

        public void Update(Location location)
        {
            _unitOfWork.LocationRepository.Update(location);
            _unitOfWork.Commit();
        }

        public void Delete(Guid locationId)
        {
            _unitOfWork.LocationRepository.Delete(locationId);
            _unitOfWork.Commit();
        }

        //public async void AssaignEmployeeToLocation(Guid EmployeeId, Guid LocationId)
        //{
        //    Employee employee = await _unitOfWork.EmployeeRepository.GetById(EmployeeId);
        //    Location location = await _unitOfWork.LocationRepository.GetById(LocationId);
        //    //employee.location = location;
        //    ////_unitOfWork.EmployeeRepository.Update(employee);
        //    //_unitOfWork.Commit();
        //}

        public bool IsInLocation(Location location, string lat, string lng)
        {
            //Location location = await _unitOfWork.LocationRepository.GetById(LocationId);
            double dist = Helpers.distance(lat, lng, location.Latitud, location.Longitude);
            if(location.radius >= dist)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
