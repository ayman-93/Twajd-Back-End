using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Twajd_Back_End.Core.Models;

namespace Twajd_Back_End.Core.Services
{
    public interface IAttendanceService
    {
        void AddAttendance(Attendance attendance);
        Task<IEnumerable<Attendance>> GetByEmplyeeId(Guid employeeId);
        Task<IEnumerable<Attendance>> GetByCompanyId(Guid CompanyId);
        void Update(Attendance attendance);
        //void Delete(Guid attendanceId);
    }
}
