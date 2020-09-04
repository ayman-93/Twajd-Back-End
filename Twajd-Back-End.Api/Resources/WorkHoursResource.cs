using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Twajd_Back_End.Api.Resources
{
    public class WorkHoursDayResource
    {
        public Guid Id { get; set; }
        public string Day { get; set; }
        public string StartWork { get; set; }
        public string EndWork { get; set; }
        public double FlexibleHour { get; set; }
    }

    public class WorkHoursResource
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<WorkHoursDayResource> WorkHoursDays { get; set; }
    }
}
