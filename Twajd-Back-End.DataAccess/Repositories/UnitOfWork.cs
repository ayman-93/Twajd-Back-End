using Twajd_Back_End.Core.Models;
using Twajd_Back_End.Core.Repositories;

namespace Twajd_Back_End.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _databaseContext;

        private IRepository<Manager> _managerRepository;
        private IRepository<Company> _companyRepository;
        private IRepository<Employee> _employeeRepository;
        private IRepository<Location> _locationRepository;
        private IRepository<Attendance> _attendanceRepository;

        public UnitOfWork(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public IRepository<Manager> ManagerRepository
        {
            get { return _managerRepository = _managerRepository ?? new Repository<Manager>(_databaseContext); }
        }

        public IRepository<Company> CompanyRepository
        {
            get { return _companyRepository = _companyRepository ?? new Repository<Company>(_databaseContext); }
        }

        public IRepository<Employee> EmployeeRepository
        {
            get { return _employeeRepository = _employeeRepository ?? new Repository<Employee>(_databaseContext); }
        }

        public IRepository<Location> LocationRepository
        {
            get { return _locationRepository = _locationRepository ?? new Repository<Location>(_databaseContext); }
        }

        public IRepository<Attendance> AttendanceRepository
        {
            get { return _attendanceRepository = _attendanceRepository ?? new Repository<Attendance>(_databaseContext); }
        }

        public void Commit()
        {
            _databaseContext.SaveChanges();
        }

        public void Rollback()
        {
            _databaseContext.Dispose();
        }
    }
}
