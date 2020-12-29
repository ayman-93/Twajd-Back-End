using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Twajd_Back_End.Core.Models;

namespace Twajd_Back_End.Core.Services
{
    public interface ILocationsService
    {
        Task<IEnumerable<Location>> Get(Guid managerId);
        Task<Location> GetById(Guid locationId);
        void AddLocation(Location location);
        void Update(Location location);
        void Delete(Guid locationId);
        //void AssaignEmployeeToLocation(Guid EmployeeId, Guid LocationId);
        bool IsInLocation(Location location, string lat, string lng);
    }
}
