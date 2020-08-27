using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Twajd_Back_End.Core.Models.Auth;

namespace Twajd_Back_End.Core.Models
{
    public class Manager : BaseEntity
    {
        [ForeignKey("ApplicationUser")]
        public Guid ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        [Column(TypeName = "varchar(80)")]
        public string FullName { get; set; }
        public Guid CompanyId { get; set; }
        public virtual Company Company { get; set; }

        [ForeignKey("ManagerId")]
        public virtual ICollection<HourWork> HourWorks { get; set; }
}
}
