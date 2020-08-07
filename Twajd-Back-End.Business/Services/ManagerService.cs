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

        public async Task<Manager> GetManagerByApplicationUserId(Guid applicationUserId)
        {
            var manager = await _unitOfWork.ManagerRepository.Get(filter: mang => mang.ApplicationUserId == applicationUserId);
            return manager.FirstOrDefault();
        }
    }
}
