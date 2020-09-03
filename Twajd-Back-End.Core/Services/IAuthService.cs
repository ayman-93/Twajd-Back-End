using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Twajd_Back_End.Core.Models.Auth;


namespace Twajd_Back_End.Core.Services
{
    public interface IAuthService
    {
        string GenerateJwt(ApplicationUser user, IList<string> roles);

        Task<bool> IsCurrentActiveToken();
        Task DeactivateCurrentAsync();
        Task<bool> IsActiveAsync(string token);
        Task DeactivateAsync(string token);
    }
}
