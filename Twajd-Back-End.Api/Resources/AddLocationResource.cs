using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Twajd_Back_End.Api.Resources
{
    public class AddLocationResource
    {
        public string Name { get; set; }
        public string Longitude { get; set; }
        public string Latitud { get; set; }
        public float radius { get; set; }
    }
}
