
using System.ComponentModel.DataAnnotations;

namespace Twajd_Back_End.Api.Resources
{
    public class UserLoginResource
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
