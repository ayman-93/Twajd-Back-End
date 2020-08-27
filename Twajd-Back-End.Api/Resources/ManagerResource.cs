using System;
using System.Collections.Generic;
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
}
