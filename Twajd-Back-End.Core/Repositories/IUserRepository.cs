using System.Threading.Tasks;
using Twajd_Back_End.Core.Models;

namespace Twajd_Back_End.Core.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByName(string firstName);
    }
}
