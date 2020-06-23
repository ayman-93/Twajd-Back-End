using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Twajd_Back_End.Core.Models
{
    public class Employee : BaseEntity
    {
        public string FullName { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string JobTitle { get; set; }
        [ForeignKey("Id")]
        public Guid CompanyId { get; set; }
        public string JobId { get; set; }
    }
}
