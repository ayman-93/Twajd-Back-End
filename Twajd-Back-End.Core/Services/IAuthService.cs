using System;
using System.Collections.Generic;
using System.Text;
using Twajd_Back_End.Core.Models.Auth;

namespace Twajd_Back_End.Core.Services
{
    public interface IAuthService
    {
        string GenerateJwt(ApplicationUser user, IList<string> roles);
    }
}
