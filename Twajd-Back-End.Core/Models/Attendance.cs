﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Twajd_Back_End.Core.Models
{
    public class Attendance : BaseEntity
    {
        public Guid EmployeeId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan PresentTime { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public bool Status { get; set; }
        public Guid CompanyId { get; set; }
        public Guid LocationId { get; set; }
        public Guid HourWorkId { get; set; }

    }
}
