using System.Threading.Tasks;
using Twajd_Back_End.Core.Models;
using Twajd_Back_End.DataAccess.Repository;

namespace Twajd_Back_End.DataAccess.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByName(string firstName);
    }
}
