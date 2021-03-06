﻿using System;
using System.Collections.Generic;
using System.Linq;
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

        public void AddEmployee(Employee entity)
        {
            _unitOfWork.EmployeeRepository.Insert(entity);
            _unitOfWork.Commit();
        }

        public void AddEmployees(Employee[] entities)
        {
            _unitOfWork.EmployeeRepository.InsertRange(entities);
            _unitOfWork.Commit();
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByCompanyId(Guid CompanyId)
        {
            IEnumerable<Employee> employees = await _unitOfWork.EmployeeRepository.Get(filter: emp => emp.CompanyId == CompanyId);
            return employees;
        }

        public async Task<Employee> GetEmployeeByApplicationUserId(Guid applicationUserId)
        {
            var employee = await _unitOfWork.EmployeeRepository.Get(filter: emp => emp.ApplicationUserId == applicationUserId, includeProperties: "Company,ApplicationUser,Location,WorkHours,Attendances");
            
            var orderedAttendEmp = employee.FirstOrDefault().Attendances.OrderBy(a => a.CreatedAt).ToList();
            employee.FirstOrDefault().Attendances = orderedAttendEmp;
            return employee.FirstOrDefault();
        }

        public async Task<Employee> GetById(Guid employeeId)
        {
            Employee employee = await _unitOfWork.EmployeeRepository.GetById(employeeId, includeProperties: "Company,ApplicationUser");
            return employee;
        }

        public void Update(Employee employee)
        {
            _unitOfWork.EmployeeRepository.Update(employee);
            _unitOfWork.Commit();
        }

        public void DeleteEmployeeById(Guid employeeId)
        {
            _unitOfWork.EmployeeRepository.Delete(employeeId);
            _unitOfWork.Commit();
        }
    }
}
