using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Twajd_Back_End.Core.Models;
using Twajd_Back_End.Core.Repositories;
using Twajd_Back_End.Core.Services;

namespace Twajd_Back_End.Business.Services
{
    public class OwnerService : IOwnerService
    {
        private IUnitOfWork _unitOfWork;

        public OwnerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Manager> addManagerAndCompany(Manager manager, Guid applicationUserId, Guid companyId)
        {
            manager.ApplicationUserId = applicationUserId;
            manager.CompanyId = companyId;
            _unitOfWork.ManagerRepository.Insert(manager);
            _unitOfWork.Commit();
            return await _unitOfWork.ManagerRepository.GetById(manager.Id);
        }
    }
}
