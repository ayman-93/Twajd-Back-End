using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Twajd_Back_End.Core.Models;

namespace Twajd_Back_End.Core.Services
{
    public interface IAttendanceService
    {
        Task<IEnumerable<Attendance>> GetCompanyId(Guid companyId);
        Task<IEnumerable<Attendance>> GetByEmplyeeId(Guid employeeId);
        Task<Location> GetById(Guid attendanceId);
        void AddAttendance(Attendance attendance);
        void Update(Attendance attendance);
        //void Delete(Guid attendanceId);
    }
}
