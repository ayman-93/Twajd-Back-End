using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Twajd_Back_End.Core.Models;

namespace Twajd_Back_End.Core.Services
{
    public interface IManagerService
    {
        Task<IEnumerable<Manager>> GetAll();
        Task<Manager> GetManagerByApplicationUserId(Guid applicationUserId);
        void addManagerAndCompany(Manager manager, Guid applicationUserId, Company company);
        Task<Manager> GetManagerById(Guid managerId);
    }
}
