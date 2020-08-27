using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Twajd_Back_End.Core.Models;

namespace Twajd_Back_End.Core.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetEmployeesByCompanyId(Guid CompanyId);
        Task<Employee> GetEmployeeByApplicationUserId(Guid applicationUserId);
        Task<Employee> GetEmployeeById(Guid Id);
        void AddEmployee(Employee entity);
        void AddEmployees(Employee[] entity);
        void Update(Employee employee);
        void DeleteEmployeeById(Guid employeeId);
    }
}
