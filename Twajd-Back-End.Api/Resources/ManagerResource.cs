using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Twajd_Back_End.Api.Resources
{
    public class ManagerResource
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string CompanyName { get; set; }

        public int NumberOfEmployees { get; set; }

        public string PackageType { get; set; }
        public string Token { get; set; }
    }
    public class AddMangerResource
    {
        public string FullName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public string CompanyName { get; set; }
        public string PackageType { get; set; }
    }
}
