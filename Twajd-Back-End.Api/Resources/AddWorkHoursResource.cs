using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Twajd_Back_End.Api.Resources
{
    public class AddWorkHoursDayResource
    {
        public DayOfWeek Day { get; set; }
        public string StartWork { get; set; }
        public string EndWork { get; set; }
        public double FlexibleHour { get; set; }
    }

    public class AddWorkHoursResource
    {
        public string Name { get; set; }
        
        public IEnumerable<AddWorkHoursDayResource> WorkHoursDays { get; set; }
    }

    
}
