using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Twajd_Back_End.Core.Models;
using Twajd_Back_End.DataAccess.Repository;

namespace Twajd_Back_End.Business.Services.Impl
{
    public class EmployeeService : IEmployeeService
    {
        private IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Employee> AddEmployee(Employee entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Employee> GetAllEmployees(Guid CompanyId)
        {
            throw new NotImplementedException();
        }

        public async Task<Employee> GetEmployeeById(Guid Id)
        {
            Employee employee = await _unitOfWork.EmployeeRepository.GetById(Id);
            return employee;
        }
    }
}
