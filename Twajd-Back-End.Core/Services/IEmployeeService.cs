using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Twajd_Back_End.Core.Models;

namespace Twajd_Back_End.Core.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetEmployeesByCompanyId(Guid CompanyId);
        Task<Employee> GetEmployeeByApplicationUserId(Guid applicationUserId);
        Task<Employee> GetEmployeeById(Guid Id);
        Task<Employee> AddEmployee(Employee entity);

    }
}
