using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Twajd_Back_End.Core.Models;
using Twajd_Back_End.Core.Repositories;
using Twajd_Back_End.Core.Services;

namespace Twajd_Back_End.Business.Services
{
    public class CompanyService : ICompanyService
    {
        private IUnitOfWork _unitOfWork;

        public CompanyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async void IncrementEmployeeNumber(Guid CompanyId)
        {
            Company company = await _unitOfWork.CompanyRepository.GetById(CompanyId);
            company.NumberOfEmployees++;
            //_unitOfWork.Commit();
        }

        public async void DecrementEmployeeNumber(Guid CompanyId)
        {
            Company company = await _unitOfWork.CompanyRepository.GetById(CompanyId);
            company.NumberOfEmployees--;
            //_unitOfWork.Commit();
        }

        public async Task<IEnumerable<Company>> Get() => await _unitOfWork.CompanyRepository.Get(includeProperties: "Employees");

        public async Task<Company> GetCompanyById(Guid Id) => await _unitOfWork.CompanyRepository.GetById(Id, includeProperties: "Employees,Locations,WorkHours");

        public async void Activate(Guid companyId)
        {
            Company company = await _unitOfWork.CompanyRepository.GetById(companyId);
            company.IsActive = true;
        }

        public async void Deactivate(Guid companyId)
        {
            Company company = await _unitOfWork.CompanyRepository.GetById(companyId);
            company.IsActive = false;
        }
    }
}
