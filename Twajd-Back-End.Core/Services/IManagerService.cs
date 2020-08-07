using System;
using System.Threading.Tasks;
using Twajd_Back_End.Core.Models;

namespace Twajd_Back_End.Core.Services
{
    public interface IManagerService
    {
        Task<Manager> GetManagerByApplicationUserId(Guid applicationUserId);
    }
}
