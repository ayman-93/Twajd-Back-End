using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Twajd_Back_End.Core.Models
{
    public class Attendance : BaseEntity
    {
        public Guid EmployeeId { get; set; }
        public DateTime Date { get; set; }
        public DateTime PresentTime { get; set; }
        public DateTime DepartureTime { get; set; }
        public Boolean Status { get; set; }
        public Guid CompanyId { get; set; }
        public Guid LocationId { get; set; }
        public Guid HourWorkId { get; set; }

    }
}
