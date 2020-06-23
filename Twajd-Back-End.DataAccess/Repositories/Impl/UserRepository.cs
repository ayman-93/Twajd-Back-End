using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Twajd_Back_End.Core.Models;

namespace Twajd_Back_End.DataAccess.Repositories.Impl
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DatabaseContext context) : base(context) { }
        public async Task<User> GetByName(string firstName)
        {
            return await context.Set<User>().FirstOrDefaultAsync(user => user.FirstName == firstName);
        }
    }
}
