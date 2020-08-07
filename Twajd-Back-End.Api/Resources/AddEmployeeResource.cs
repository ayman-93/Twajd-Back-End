using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Twajd_Back_End.Api.Resources
{
    public class AddEmployeeResource
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string JobTitle { get; set; }
        public string JobId { get; set; }
    }
}
