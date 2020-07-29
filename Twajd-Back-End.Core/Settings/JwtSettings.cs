using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Twajd_Back_End.Core.Settings
{
    public class JwtSettings
    {
        public string Secret { set; get; }
        public double ExpirationInDays { set; get; }
        public string Issuer { set; get; }
    }
}
