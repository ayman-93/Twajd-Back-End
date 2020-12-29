using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twajd_Back_End.Core.Models;
using Twajd_Back_End.Core.Repositories;
using Twajd_Back_End.Core.Services;

namespace Twajd_Back_End.Business.Services
{
    public class ManagerService : IManagerService
    {
        private IUnitOfWork _unitOfWork;
        public ManagerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Manager>> GetAll()
        {
            IEnumerable<Manager> manager = await _unitOfWork.ManagerRepository.Get(includeProperties: "Company,ApplicationUser");
            return manager;
        }

        public async Task<Manager> GetManagerByApplicationUserId(Guid applicationUserId)
        {
            var manager = await _unitOfWork.ManagerRepository.Get(filter: mang => mang.ApplicationUserId == applicationUserId, includeProperties: "Company,ApplicationUser");
            return manager.FirstOrDefault();
        }

        public async Task<Manager> GetManagerById(Guid managerId)
        {
            var manager = await _unitOfWork.ManagerRepository.GetById(managerId, includeProperties: "Company,ApplicationUser");
            return manager;
        }

        public void addManagerAndCompany(Manager manager, Guid applicationUserId, Company company)
        {
            manager.ApplicationUserId = applicationUserId;
            _unitOfWork.CompanyRepository.Insert(company);
            manager.CompanyId = company.Id;
            _unitOfWork.ManagerRepository.Insert(manager);
            _unitOfWork.Commit();
        }   
    }
}
