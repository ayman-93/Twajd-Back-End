using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Twajd_Back_End.Core.Models;

namespace Twajd_Back_End.Core.Services
{
    public interface ICompanyService
    {
        void IncrementEmployeeNumber(Guid companyId);
        void DecrementEmployeeNumber(Guid companyId);
        Task<IEnumerable<Company>> Get();
        Task<Company> GetCompanyById(Guid id);
        void Activate(Guid companyId);
        void Deactivate(Guid companyId);
    }
}
