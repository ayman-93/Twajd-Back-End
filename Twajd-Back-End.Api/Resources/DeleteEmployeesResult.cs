using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Twajd_Back_End.Api.Resources
{
    public class DeleteEmployeesResult
    {
        public List<Guid> DeletedUsers { get; } = new List<Guid>();
        public List<Guid> UnfoundUsers { get; } = new List<Guid>();
    }
}
