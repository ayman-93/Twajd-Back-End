using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Twajd_Back_End.Core.Models;

namespace Twajd_Back_End.Core.Repositories
{
    public interface ICompanyRepository : IRepository<Company>
    {
        IEnumerable<Company> GetComapnaiesWithEmployees();
        new Task<Company> GetById(Guid Id);
    }
}
