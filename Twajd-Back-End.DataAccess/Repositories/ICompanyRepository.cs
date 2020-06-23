using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Twajd_Back_End.Core.Models;
using Twajd_Back_End.DataAccess.Repository;

namespace Twajd_Back_End.DataAccess.Repositories
{
    public interface ICompanyRepository : IRepository<Company>
    {
        IEnumerable<Company> GetComapnaiesWithEmployees();
        new Task<Company> GetById(Guid Id);
    }
}
