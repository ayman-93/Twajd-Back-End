using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Twajd_Back_End.Core.Models;
using Twajd_Back_End.Core.Repositories;
using Twajd_Back_End.Core.Services;

namespace Twajd_Back_End.Business.Services
{
    public class EmployeeService : IEmployeeService
    {
        private IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Employee> AddEmployee(Employee entity)
        {
            _unitOfWork.EmployeeRepository.Insert(entity);
            _unitOfWork.Commit();
            return await _unitOfWork.EmployeeRepository.GetById(entity.Id);
        }

        

        public async Task<Employee> GetEmployeeById(Guid Id)
        {
            Employee employee = await _unitOfWork.EmployeeRepository.GetById(Id);
            return employee;
        }
    }
}
