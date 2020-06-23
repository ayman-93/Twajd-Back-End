using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twajd_Back_End.Core.Models;

namespace Twajd_Back_End.DataAccess.Repositories.Impl
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public CompanyRepository(DatabaseContext context) : base(context) { }

        public IEnumerable<Company> GetComapnaiesWithEmployees()
        {
            var ComapnaiesWithEmployees = context.Companies.Include(camp => camp.Employees);
            return ComapnaiesWithEmployees;
        }

        public new async Task<Company> GetById(Guid Id)
        {
           Company company = await context.Companies.Where(camp => camp.Id == Id).Include(camp => camp.Employees).FirstOrDefaultAsync();
            return company;
        }

    }
}
