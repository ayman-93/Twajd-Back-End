using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Twajd_Back_End.Api.Resources
{
    public class EmployeeResource
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        //public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string JobTitle { get; set; }
        public string JobId { get; set; }
        public string CompanyName { get; set; }
        public string LocationName { get; set; }
        public string WorkHoursName { get; set; }
        public bool Status { get; set; }
        public string LastSubmit { get; set; }
        public string Token { get; set; }
    }
    public class AddEmployeeResource
    {
        public string FullName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public string JobTitle { get; set; }
        public string JobId { get; set; }
    }
    public class DeleteEmployeesResult
    {
        public List<Guid> DeletedUsers { get; } = new List<Guid>();
        public List<Guid> UnfoundUsers { get; } = new List<Guid>();
    }
}
