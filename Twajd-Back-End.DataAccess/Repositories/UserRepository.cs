//using Microsoft.EntityFrameworkCore;
//using System.Threading.Tasks;
//using Twajd_Back_End.Core.Models;
//using Twajd_Back_End.Core.Repositories;

//namespace Twajd_Back_End.DataAccess.Repositories
//{
    //public class UserRepository : Repository<FaskeUser>, IUserRepository
    //{
    //    public UserRepository(DatabaseContext context) : base(context) { }
    //    public async Task<FaskeUser> GetByName(string firstName)
    //    {
    //        return await context.Set<FaskeUser>().FirstOrDefaultAsync(user => user.FirstName == firstName);
    //    }
    //}
//}
