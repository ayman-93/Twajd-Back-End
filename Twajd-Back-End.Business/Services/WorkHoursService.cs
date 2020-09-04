using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Twajd_Back_End.Core.Models;
using Twajd_Back_End.Core.Repositories;
using Twajd_Back_End.Core.Services;

namespace Twajd_Back_End.Business.Services
{
    public class WorkHoursService : IWorkHoursService
    {
        private readonly IUnitOfWork _unitOfWork;
        public WorkHoursService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<WorkHours>> Get(Guid managerId)
        {
            return await _unitOfWork.WorkHoursRepository.Get(filter: wh => wh.ManagerId == managerId, includeProperties: "WorkHoursDays");
        }
        public async Task<WorkHours> GetById(Guid WorkHoursId)
        {
            return await _unitOfWork.WorkHoursRepository.GetById(WorkHoursId, includeProperties: "WorkHoursDays");
        }

        public void AddWorkHours(WorkHours workHours)
        {
            
            _unitOfWork.WorkHoursRepository.Insert(workHours);
            _unitOfWork.Commit();
        }

        public void Update(WorkHours workHours)
        {
            _unitOfWork.WorkHoursRepository.Update(workHours);
            _unitOfWork.Commit();
        }

        public void Delete(Guid workHoursId)
        {
            _unitOfWork.WorkHoursRepository.Delete(workHoursId);
            _unitOfWork.Commit();
        }
    }
}
