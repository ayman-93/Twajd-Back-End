using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Twajd_Back_End.Core.Models
{
    public class HourWork : BaseEntity
    {
        public string Name { get; set; }
        public Guid ManagerId { get; set; }
        public DateTime StartWork { get; set; }
        public DateTime EndWork { get; set; }
        public DateTime FlexibleHour { get; set; }
        public string day { get; set; }

        [ForeignKey("HourWorkId")]
        public virtual ICollection<Attendance> Attendances { get; set; }
    }
}
