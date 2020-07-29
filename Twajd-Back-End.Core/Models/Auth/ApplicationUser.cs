using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Twajd_Back_End.Core.Models.Auth
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public virtual Employee Employee { get; set; }
        public virtual Manager Manager { get; set; }
    }
}
