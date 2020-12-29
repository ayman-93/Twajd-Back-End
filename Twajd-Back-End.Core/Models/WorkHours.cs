using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Twajd_Back_End.Core.Models
{
    public class WorkHours : BaseEntity
    {
        public string Name { get; set; }
        [ForeignKey("HourWorkId")]
        public virtual ICollection<WorkHoursDay> WorkHoursDays { get; set; }
        public virtual Guid CompanyId { get; set; }
        [ForeignKey("HourWorkId")]
        public virtual ICollection<Attendance> Attendances { get; set; }
    }

    public class WorkHoursDay : BaseEntity
    {
        public Guid HourWorkId { get; set; }
        public DayOfWeek Day { get; set; }
        public TimeSpan StartWork { get; set; }
        public TimeSpan EndWork { get; set; }
        public TimeSpan FlexibleHour { get; set; }
    }
}
