using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Twajd_Back_End.Core.Models
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }
        // this should be remove it should be inside the package info.. m
        public int NumberOfEmployees { get; set; }
        public string PackageType { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime subscriptionDate { get; set; }

        [ForeignKey("CompanyId")]
        public virtual ICollection<Manager> Managers { get; set; }
        [ForeignKey("CompanyId")]
        public virtual ICollection<Employee> Employees { get; set; }
        [ForeignKey("CompanyId")]
        public virtual ICollection<Location> Locations { get; set; }
        [ForeignKey("CompanyId")]
        public virtual ICollection<WorkHours> WorkHours { get; set; }
        [ForeignKey("CompanyId")]
        public virtual ICollection<Attendance> Attendances { get; set; }
    }
}
