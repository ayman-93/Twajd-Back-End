using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Twajd_Back_End.Api.Resources
{
    public class EmployeeLoginResponse
    {
        public string FullName { get; set; }
        public string JobTitle { get; set; }
        public string JobId { get; set; }
        public string Token { get; set; }
    }
}
