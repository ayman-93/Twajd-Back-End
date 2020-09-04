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
    [Route("twajd-api/[controller]")]
    [ApiController]
    public class WorkHoursController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWorkHoursService _workHoursService;
        private readonly IManagerService _managerService;
        public WorkHoursController(IWorkHoursService workHoursService, IMapper mapper, IManagerService managerService)
        {
            _mapper = mapper;
            _workHoursService = workHoursService;
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

            IEnumerable<WorkHours> workHours = await _workHoursService.Get(manager.Id);
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
            workHoursWeek.ManagerId = manager.Id;
            var test = workHoursWeek;
            _workHoursService.AddWorkHours(workHoursWeek);
            return Ok();
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

            if(oldWorkHours == null)
            {
                return NotFound();
            }

            if(oldWorkHours.ManagerId == manager.Id)
            {

                WorkHours workHoursToUpdate = _mapper.Map<AddWorkHoursResource, WorkHours>(workHours, oldWorkHours);
                _workHoursService.Update(workHoursToUpdate);
                return Ok();
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

            if (oldWorkHours.ManagerId == manager.Id)
            {
                _workHoursService.Delete(id);
                return Ok();
            }

            return Unauthorized();
        }
    }
}
