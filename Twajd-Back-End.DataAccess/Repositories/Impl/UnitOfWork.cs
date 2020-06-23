using Twajd_Back_End.Core.Models;
using Twajd_Back_End.DataAccess.Repository;

namespace Twajd_Back_End.DataAccess.Repositories.Impl
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _databaseContext;
        private IUserRepository _userRepository;
        private ICompanyRepository _companyRepository;
        private IRepository<Company> _ccompanyRepository;
        private IRepository<Employee> _employeeRepository;

        public UnitOfWork(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public IUserRepository UserRepository
        {
            get { return _userRepository = _userRepository ?? new UserRepository(_databaseContext); }
        }

        public ICompanyRepository CompanyRepository
        {
            get { return _companyRepository = _companyRepository ?? new CompanyRepository(_databaseContext); }
        }

        public IRepository<Company> CCompanyRepository
        {
            get { return _ccompanyRepository = _ccompanyRepository ?? new Repository<Company>(_databaseContext); }
        }

        public IRepository<Employee> EmployeeRepository
        {
            get { return _employeeRepository = _employeeRepository ?? new Repository<Employee>(_databaseContext); }
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
