using Twajd_Back_End.Core.Models;

namespace Twajd_Back_End.Core.Repositories
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
