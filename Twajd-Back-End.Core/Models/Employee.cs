using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Twajd_Back_End.Core.Models.Auth;

namespace Twajd_Back_End.Core.Models
{
    public class Employee : BaseEntity
    {
        [ForeignKey("ApplicationUser")]
        public Guid ApplicationUserId { get; set; }
        public string FullName { get; set; }
        //public string MobileNumber { get; set; }
        //public string Email { get; set; }
        public string JobTitle { get; set; }
        public Guid CompanyId { get; set; }
        public string JobId { get; set; }
        [ForeignKey("EmployeeId")]
        public virtual ICollection<Attendance> Attendances { get; set; }

        // public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
