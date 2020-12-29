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
    public class LocationsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILocationsService _locationService;
        private readonly IEmployeeService _employeeService;
        private readonly IManagerService _managerService;
        public LocationsController(ILocationsService locationService, IEmployeeService employeeService, IMapper mapper, IManagerService managerService)
        {
            _mapper = mapper;
            _locationService = locationService;
            _employeeService = employeeService;
            _managerService = managerService;
        }

        /// <summary>
        /// Get all Work Hours, used by manager.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = Role.Manager)]
        public async Task<ActionResult<IEnumerable<LocationResource>>> Get()
        {
            Guid managerApplicationUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Manager manager = await _managerService.GetManagerByApplicationUserId(managerApplicationUserId);

            IEnumerable<Location> Location = await _locationService.Get(manager.Id);
            var LocationResponse = _mapper.Map<IEnumerable<Location>, IEnumerable<LocationResource>>(Location);
            return Ok(LocationResponse);
        }


        /// <summary>
        /// Create new Work Hours, used by manager.
        /// StartWork and EndWork are strings: "1:01 AM" or "1:01 PM".
        /// FlexibleHour is a double 1.5 mean 1 hour and 30 minutes.
        /// </summary>
        /// <param name="LocationRes"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = Role.Manager)]
        public async Task<ActionResult> Post(AddLocationResource LocationRes)
        {
            Guid managerApplicationUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Manager manager = await _managerService.GetManagerByApplicationUserId(managerApplicationUserId);

            Location location = _mapper.Map<Location>(LocationRes);
            location.manager = manager;
            location.CompanyId = manager.CompanyId;
            var test = location;
            _locationService.AddLocation(location);
            return Ok();
        }

        /// <summary>
        /// Update Work Hours, used by manager.
        /// StartWork and EndWork are strings: "1:01 am" or "1:01 pm".
        /// FlexibleHour is a double 1.5 mean 1 hour and 30 minutes.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Location"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = Role.Manager)]
        public async Task<ActionResult> Put(Guid id, AddLocationResource Location)
        {
            Guid managerApplicationUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Manager manager = await _managerService.GetManagerByApplicationUserId(managerApplicationUserId);

            Location oldLocation = await _locationService.GetById(id);

            if (oldLocation == null)
            {
                return NotFound();
            }

            if (oldLocation.manager.Id == manager.Id)
            {

                Location LocationToUpdate = _mapper.Map<AddLocationResource, Location>(Location, oldLocation);
                _locationService.Update(LocationToUpdate);
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

            Location oldLocation = await _locationService.GetById(id);

            if (oldLocation == null)
            {
                return NotFound();
            }

            if (oldLocation.manager.Id == manager.Id)
            {
                _locationService.Delete(id);
                return Ok();
            }

            return Unauthorized();
        }

        [HttpPost("Add-employee")]
        [Authorize(Roles = Role.Manager)]
        public async Task<ActionResult> AssignEmployee(Guid EmpolyeeId, Guid LocationId)
        {
            Employee employee = await _employeeService.GetById(EmpolyeeId);
            employee.Location = await _locationService.GetById(LocationId);
            _employeeService.Update(employee);
            //_locationService.AssaignEmployeeToLocation(EmpolyeeId, LocationId);
            return Ok();
        }

    }
}
