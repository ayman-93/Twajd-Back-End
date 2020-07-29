using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Twajd_Back_End.Core.Models
{
    public class Manager : BaseEntity
    {
        [ForeignKey("ApplicationUser")]
        public Guid ApplicationUserId { get; set; }
        [Column(TypeName = "varchar(80)")]
        public string FullName { get; set; }
        public Guid CompanyId { get; set; }

        [ForeignKey("ManagerId")]
        public virtual ICollection<HourWork> HourWorks { get; set; }
}
}
