using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Twajd_Back_End.Core.Models;

namespace Twajd_Back_End.Core.Services
{
    public interface IWorkHoursService
    {
        Task<IEnumerable<WorkHours>> Get(Guid managerId);
        Task<WorkHours> GetById(Guid WorkHoursId);
        void AddWorkHours(WorkHours workHours);
        void Update(WorkHours workHours);
        void Delete(Guid workHoursId);
    }
}
