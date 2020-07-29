using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Twajd_Back_End.Core.Models
{
    public class Location : BaseEntity
    {
        public string Name { get; set; }
        public Guid CompanyId { get; set; }
        public string Longitude { get; set; }
        public string Latitud { get; set; }
        public float radius { get; set; }

        [ForeignKey("LocationId")]
        public virtual ICollection<Attendance> Attendances { get; set; }
    }
}
