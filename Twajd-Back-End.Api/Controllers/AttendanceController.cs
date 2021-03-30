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
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IMapper _mapper;
        //private readonly ILocationService _locationService;
        private readonly IAttendanceService _attendanceService;
        private readonly IEmployeeService _employeeService;
        private readonly IManagerService _managerService;
        private readonly ILocationsService _locationsService;
        private readonly IWorkHoursService _workHoursService;
        private readonly UserManager<ApplicationUser> _userManager;
        public AttendanceController(IAttendanceService attendanceService, IEmployeeService employeeService, IManagerService managerService, ILocationsService locationsService, IWorkHoursService workHoursService, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _mapper = mapper;
            _attendanceService = attendanceService;
            _employeeService = employeeService;
            _managerService = managerService;
            _locationsService = locationsService;
            _workHoursService = workHoursService;
            _userManager = userManager;
        }


        /// <summary>
        /// Get all attendances of the employee or all attendances of employees depends on the user, used by manager and employee
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = Role.Employee + "," + Role.Manager)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PresentAndLeaveResource>>> Get()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Contains("Manager"))
            {
                Manager manager = await _managerService.GetManagerByApplicationUserId(user.Id);
                var attendance = await _attendanceService.GetByCompanyId(manager.CompanyId);
                var atendRespon = _mapper.Map<IEnumerable<Attendance>, IEnumerable<PresentAndLeaveResource>>(attendance);
                return Ok(atendRespon);
            }
            else if (roles.Contains("Employee"))
            {
                Employee emp = await _employeeService.GetEmployeeByApplicationUserId(user.Id);
                var attendance = await _attendanceService.GetByEmplyeeId(emp.Id);
                var atendRespon = _mapper.Map<IEnumerable<Attendance>, IEnumerable<PresentAndLeaveResource>>(attendance);
                var withKey = _mapper.Map<PresentAndLeaveResourceArray>(atendRespon);
                return Ok(withKey);
            }
            else
            {
                return BadRequest();
            }

        }

        /// <summary>
        /// Get an employee attendance by employee id, used by manager.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [Authorize(Roles = Role.Manager)]
        [HttpGet("{employeeId}")]
        public async Task<ActionResult<IEnumerable<PresentAndLeaveResource>>> Get(Guid employeeId)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            Manager manager = await _managerService.GetManagerByApplicationUserId(user.Id);
            var attendance = await _attendanceService.GetByEmplyeeId(employeeId);
            if(attendance.FirstOrDefault().CompanyId == manager.CompanyId)
            {
            var atendRespon = _mapper.Map<IEnumerable<Attendance>, IEnumerable<PresentAndLeaveResource>>(attendance);
            return Ok(atendRespon);
            } else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Attend of Leave depends on the status of the user, used by employee.
        /// </summary>
        /// <param name="attendanceResource"></param>
        /// <returns></returns>
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
                    var empAttends = await _attendanceService.GetByEmplyeeId(emp.Id);
                    var empAttend = empAttends.LastOrDefault();

                    if (empAttend != null && empAttend.Status)
                    {
                        // he is present and want to leave
                        empAttend.Status = false;
                        empAttend.DepartureTime = DateTime.Now.TimeOfDay;
                        empAttend.UpdateAt = DateTime.Now;
                        var test = (empAttend.CreatedAt - empAttend.UpdateAt).TotalHours;
                        _attendanceService.Update(empAttend);
                        var atendRespon = _mapper.Map<Attendance, PresentAndLeaveResource>(empAttend);
                        return Ok(atendRespon);
                    }
                    else
                    {
                        // he is out and want to present
                        var newAttendances = new Attendance() { EmployeeId = emp.Id, LocationId = emp.Location.Id, CompanyId = emp.CompanyId, HourWorkId = emp.WorkHours.Id };
                        _attendanceService.AddAttendance(newAttendances);
                        var atendRespon = _mapper.Map<Attendance, PresentAndLeaveResource>(newAttendances);
                        return Ok(atendRespon);
                    }


                    //WorkHours workHours = await _workHoursService.GetById(emp.WorkHours.Id);
                    //WorkHoursDay workHoursDays = workHours.WorkHoursDays.FirstOrDefault(dayofweek => dayofweek.Day == DateTime.Today.DayOfWeek);
                    //if (workHoursDays.StartWork <= DateTime.Now.TimeOfDay)
                    //{
                    //    return Ok();
                    //} else
                    //{
                    //    return BadRequest();
                    //}
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                if (e.GetType().Name == "FormatException")
                {
                    return BadRequest(new CustomMessge() { Message = "FormatException" });
                }
                else
                {
                    return Unauthorized(e);
                }
            }
        }

    }
}
