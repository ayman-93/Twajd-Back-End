using Twajd_Back_End.Core.Models;

namespace Twajd_Back_End.Core.Repositories
{
    public interface IUnitOfWork
    {
        //IUserRepository UserRepository { get; }
        //ICompanyRepository CompanyRepository { get; }
        IRepository<Manager> ManagerRepository { get; }
        IRepository<Company> CompanyRepository { get; }
        IRepository<Employee> EmployeeRepository { get; }
        IRepository<WorkHours> WorkHoursRepository { get; }
        IRepository<Attendance> AttendanceRepository { get; }
        IRepository<Location> LocationRepository { get; }
        public void Commit();
        public void Rollback();
    }
}
