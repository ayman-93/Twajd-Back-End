using Twajd_Back_End.Core.Models;
using Twajd_Back_End.DataAccess.Repositories;

namespace Twajd_Back_End.DataAccess.Repository
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        ICompanyRepository CompanyRepository { get; }
        IRepository<Company> CCompanyRepository { get; }
        IRepository<Employee> EmployeeRepository { get; }
        public void Commit();
        public void Rollback();
    }
}
