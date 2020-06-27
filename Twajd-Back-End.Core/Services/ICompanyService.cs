using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Twajd_Back_End.Core.Models;

namespace Twajd_Back_End.Core.Services
{
    public interface ICompanyService
    {
        IEnumerable<Company> GetAllCompanys();
        Task<IEnumerable<Company>> Get();
        Task<Company> GetCompanyById(Guid Id);
        void UpdateComapny(Company company);
        Task<Company> AddCompany(Company entity);
    }
}
