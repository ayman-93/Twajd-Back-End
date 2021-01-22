using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Twajd_Back_End.Core.Models;
using Twajd_Back_End.Core.Repositories;
using Twajd_Back_End.Core.Services;

namespace Twajd_Back_End.Business.Services
{
    public class AttendanceService : IAttendanceService
    {
        private IUnitOfWork _unitOfWork;

        public AttendanceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public void AddAttendance(Attendance attendance)
        {
            _unitOfWork.AttendanceRepository.Insert(attendance);
            _unitOfWork.Commit();
        }

        public async Task<IEnumerable<Attendance>> GetByEmplyeeId(Guid employeeId)
        {
            return await _unitOfWork.AttendanceRepository.Get(filter: at => at.EmployeeId == employeeId);
        }

        public async Task<IEnumerable<Attendance>> GetByCompanyId(Guid CompanyId)
        {
            return await _unitOfWork.AttendanceRepository.Get(filter: at => at.CompanyId == CompanyId);
        }


        public void Update(Attendance attendance)
        {
            _unitOfWork.AttendanceRepository.Update(attendance);
            _unitOfWork.Commit();
        }
    }
}
