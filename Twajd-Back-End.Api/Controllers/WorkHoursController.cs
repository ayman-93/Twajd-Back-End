using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    public class WorkHoursController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWorkHoursService _workHoursService;
        private readonly IEmployeeService _employeeService;
        private readonly IManagerService _managerService;
        public WorkHoursController(IWorkHoursService workHoursService, IEmployeeService employeeService, IMapper mapper, IManagerService managerService)
        {
            _mapper = mapper;
            _workHoursService = workHoursService;
            _employeeService = employeeService;
            _managerService = managerService;
        }

        /// <summary>
        /// Get all Work Hours, used by manager.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = Role.Manager)]
        public async Task<ActionResult<IEnumerable<WorkHoursResource>>> Get()
        {
            Guid managerApplicationUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Manager manager = await _managerService.GetManagerByApplicationUserId(managerApplicationUserId);

            IEnumerable<WorkHours> workHours = await _workHoursService.Get(manager.CompanyId);
            var workHoursResponse = _mapper.Map<IEnumerable<WorkHours>, IEnumerable<WorkHoursResource>>(workHours);
            return Ok(workHoursResponse);
        }


        /// <summary>
        /// Create new Work Hours, used by manager.
        /// StartWork and EndWork are strings: "1:01 AM" or "1:01 PM".
        /// FlexibleHour is a double 1.5 mean 1 hour and 30 minutes.
        /// </summary>
        /// <param name="workHours"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = Role.Manager)]
        public async Task<ActionResult> Post(AddWorkHoursResource workHours)
        {
            Guid managerApplicationUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Manager manager = await _managerService.GetManagerByApplicationUserId(managerApplicationUserId);

            WorkHours workHoursWeek = _mapper.Map<WorkHours>(workHours);
            workHoursWeek.CompanyId = manager.CompanyId;
            _workHoursService.AddWorkHours(workHoursWeek);
            return Ok(new CustomMessge() { Message = "Work Hours created successfully." });
        }

        /// <summary>
        /// Update Work Hours, used by manager.
        /// StartWork and EndWork are strings: "1:01 am" or "1:01 pm".
        /// FlexibleHour is a double 1.5 mean 1 hour and 30 minutes.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="workHours"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = Role.Manager)]
        public async Task<ActionResult> Put(Guid id, AddWorkHoursResource workHours)
        {
            Guid managerApplicationUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Manager manager = await _managerService.GetManagerByApplicationUserId(managerApplicationUserId);

            WorkHours oldWorkHours = await _workHoursService.GetById(id);

            if (oldWorkHours == null)
            {
                return NotFound(new CustomMessge() { Message = "Work Hours not found" });
            }

            if (oldWorkHours.CompanyId == manager.CompanyId)
            {

                WorkHours workHoursToUpdate = _mapper.Map<AddWorkHoursResource, WorkHours>(workHours, oldWorkHours);
                _workHoursService.Update(workHoursToUpdate);
                return Ok(new CustomMessge() { Message = String.Format("{0} Work Hours update successfully.", workHoursToUpdate.Name) });
            }
            return Unauthorized();
        }

        /// <summary>
        /// Delete Work Hours by id, used by manager.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = Role.Manager)]
        public async Task<ActionResult> Delete(Guid id)
        {
            Guid managerApplicationUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Manager manager = await _managerService.GetManagerByApplicationUserId(managerApplicationUserId);

            WorkHours oldWorkHours = await _workHoursService.GetById(id);

            if (oldWorkHours == null)
            {
                return NotFound();
            }

            if (oldWorkHours.CompanyId == manager.CompanyId)
            {
                _workHoursService.Delete(id);
                return Ok(new CustomMessge() { Message = String.Format("Work Hours with id \"{0}\" deleted successfully.", id) });
            }

            return Unauthorized();
        }

        [HttpPost("Add-employee")]
        [Authorize(Roles = Role.Manager)]
        public async Task<ActionResult> AssignEmployee(Guid workHoursId, Guid EmpolyeeId)
        {
            Guid managerApplicationUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Manager manager = await _managerService.GetManagerByApplicationUserId(managerApplicationUserId);
            Employee employee = await _employeeService.GetById(EmpolyeeId);

            if (manager.CompanyId == employee.CompanyId)
            {
                employee.WorkHours = await _workHoursService.GetById(workHoursId);
                _employeeService.Update(employee);
                return Ok(new CustomMessge() { Message = String.Format("{0} is assignd to work hours {1}.", employee.FullName, employee.WorkHours.Name) });
            }
            else
            {
                return Unauthorized();
            }
        }

    }
}
