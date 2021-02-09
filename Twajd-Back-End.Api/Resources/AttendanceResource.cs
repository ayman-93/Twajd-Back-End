using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Twajd_Back_End.Api.Resources
{
    public class AttendanceResource
    {
        public string lat { get; set; }
        public string lng { get; set; }
    }
    public class PresentAndLeaveResource
    {
        public Guid EmployeeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public string PresentTime { get; set; }
        public string DepartureTime { get; set; }
        public bool Status { get; set; }
        public string SpendTime { get; set; }
    }
}
