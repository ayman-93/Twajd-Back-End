using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Twajd_Back_End.Core.Models;
using Twajd_Back_End.Core.Repositories;
using Twajd_Back_End.Core.Services;

namespace Twajd_Back_End.Business.Services.Impl
{
    public class CompanyService : ICompanyService
    {
        private IUnitOfWork _unitOfWork;

        public CompanyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

       public IEnumerable<Company> GetAllCompanys() => _unitOfWork.CompanyRepository.GetComapnaiesWithEmployees();

        public async Task<IEnumerable<Company>> Get() => await _unitOfWork.CCompanyRepository.Get(includeProperties: "Employees");

        public async Task<Company> GetCompanyById(Guid Id) => await _unitOfWork.CCompanyRepository.GetById(Id, includeProperties: "Employees");
        public void UpdateComapny(Company company) {
            //Company dbComapny = await _unitOfWork.CCompanyRepository.GetById(company.Id);
            //dbComapny = company;
           // dbComapny.Update(company);

            _unitOfWork.CCompanyRepository.Update(company);
            _unitOfWork.Commit();
        }
        public async Task<Company> AddCompany(Company entity)
        {
            _unitOfWork.CCompanyRepository.Insert(entity);
            _unitOfWork.Commit();
            return await _unitOfWork.CCompanyRepository.GetById(entity.Id,includeProperties: "Employees");
        }

        public async void Delete(Guid id)
        {
            Company company = await _unitOfWork.CCompanyRepository.GetById(id);
        }
    }
}
