using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Twajd_Back_End.Api.Resources;
using Twajd_Back_End.Core.Models;
using Twajd_Back_End.Core.Models.Auth;
using Twajd_Back_End.Core.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Twajd_Back_End.Api.Controllers
{
    [Route("twajd-api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IMapper _mapper;
        //private readonly ILocationService _locationService;
        private readonly IAttendanceService _attendanceService;
        private readonly IEmployeeService _employeeService;
        private readonly ILocationsService _locationsService;
        private readonly IWorkHoursService _workHoursService;
        private readonly UserManager<ApplicationUser> _userManager;
        public AttendanceController(IAttendanceService attendanceService, IEmployeeService employeeService, ILocationsService locationsService, IWorkHoursService workHoursService, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _mapper = mapper;
            _attendanceService = attendanceService;
            _employeeService = employeeService;
            _locationsService = locationsService;
            _workHoursService = workHoursService;
            _userManager = userManager;
        }


        // GET: api/<AttendanceController>
        [Authorize(Roles = Role.Employee)]
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            return new string[] { "value1", "value2" };
        }

        // GET api/<AttendanceController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AttendanceController>
        [HttpPost]
        [Authorize(Roles = Role.Employee)]
        public async Task<ActionResult> Post(AttendanceResource attendanceResource)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                ApplicationUser user = await _userManager.FindByIdAsync(userId);

                Employee emp = await _employeeService.GetEmployeeByApplicationUserId(user.Id);
                bool isInLocation = _locationsService.IsInLocation(emp.Location, attendanceResource.lat, attendanceResource.lng);

                if (isInLocation)
                {
                    WorkHours workHours = await _workHoursService.GetById(emp.WorkHours.Id);
                    WorkHoursDay workHoursDays = workHours.WorkHoursDays.FirstOrDefault(dayofweek => dayofweek.Day == DateTime.Today.DayOfWeek);
                    if (workHoursDays.StartWork <= DateTime.Now.TimeOfDay)
                    {

                        return Ok();
                    } else
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch
            {
                return Unauthorized();
            }
        }

        // PUT api/<AttendanceController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AttendanceController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
