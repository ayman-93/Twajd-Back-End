using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Twajd_Back_End.Core.Models;
using Twajd_Back_End.Core.Services;

namespace Twajd_Back_End.Business.Services
{
    public class AttendanceService : IAttendanceService
    {
        public void AddAttendance(Attendance attendance)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Attendance>> GetByEmplyeeId(Guid employeeId)
        {
            throw new NotImplementedException();
        }

        public Task<Location> GetById(Guid attendanceId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Attendance>> GetCompanyId(Guid companyId)
        {
            throw new NotImplementedException();
        }

        public void Update(Attendance attendance)
        {
            throw new NotImplementedException();
        }
    }
}
