using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Twajd_Back_End.Api.Resources
{
    public class AddOwnerResource
    {
        public string Email { get; set; }

        public string FullName { get; set; }

        public string Password { get; set; }
    }
}
