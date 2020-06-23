using System;
using System.Collections.Generic;
using System.Text;

namespace Twajd_Back_End.Core.Models
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
