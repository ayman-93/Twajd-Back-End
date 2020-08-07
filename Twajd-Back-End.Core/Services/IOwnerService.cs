using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Twajd_Back_End.Core.Models;

namespace Twajd_Back_End.Core.Services
{
    public interface IOwnerService
    {
        Task<Manager> addManagerAndCompany(Manager manager, Guid applicationUserId, Guid companyId);
    }
}
