using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Twajd_Back_End.Api.Resources
{
    public class AddMangerResource
    {
        public string FullName { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }

        public string CompanyName { get; set; }

        public int NumberOfEmployees { get; set; }

        public string PackageType { get; set; }
    }
}
