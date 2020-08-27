using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Twajd_Back_End.Api.Resources;
using Twajd_Back_End.Core.Models;
using Twajd_Back_End.Core.Models.Auth;
using Twajd_Back_End.Core.Services;

namespace Twajd_Back_End.Api.Controllers
{
    [Route("twajd-api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IManagerService _managerService;
        private readonly ICompanyService _companyService;

        public ManagerController(IMapper mapper, UserManager<ApplicationUser> userManager, IManagerService managerService,
            ICompanyService companyService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _managerService = managerService;
            _companyService = companyService;
        }

        /// <summary>
        /// Create company and assign a manager to it, used by owner.
        /// </summary>
        /// <param name="mangerResource">test param</param>
        /// <returns>new manager</returns>
        [HttpPost]
        [Authorize(Roles = Role.Owner)]
        public async Task<ActionResult> CreateManager(AddMangerResource mangerResource)
        {
            ApplicationUser user = _mapper.Map<AddMangerResource, ApplicationUser>(mangerResource);
            Company company = _mapper.Map<AddMangerResource, Company>(mangerResource);
            Manager manager = _mapper.Map<AddMangerResource, Manager>(mangerResource);

            var userCreateResult = await _userManager.CreateAsync(user, mangerResource.Password);
            if (userCreateResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Role.Manager);
                //await _companyService.AddCompany(company);
                _managerService.addManagerAndCompany(manager, user.Id, company);
                return StatusCode(201);
            }

            return Problem(userCreateResult.Errors.First().Description, null, 400);
        }

        /// <summary>
        /// Get All managers, used by owner.
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = Role.Owner)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ManagerResource>>> GetManagers()
        {
            IEnumerable<Manager> managers = await _managerService.GetAll();
            IEnumerable<ManagerResource> managerResources = _mapper.Map<IEnumerable<Manager>, IEnumerable<ManagerResource>>(managers);
            return Ok(managerResources);
        }

        /// <summary>
        /// Get manager, used by Owner
        /// </summary>
        /// <param name="id">Manager id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(Roles = Role.Owner)]
        public async Task<ActionResult<ManagerResource>> GetManagerById(Guid id)
        {
            Manager manager = await _managerService.GetManagerById(id);
            ManagerResource managerResource = _mapper.Map<Manager, ManagerResource>(manager);
            return Ok(managerResource);
        }

        /// <summary>
        /// Deactivate the company and all of its users manager and employees, used by owner.
        /// </summary>
        /// <param name="id">Manager Id</param>
        /// <returns>new manager</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = Role.Owner)]
        public async Task<ActionResult> DeactivateManager(Guid id)
        {
            Manager manager = await _managerService.GetManagerById(id);

            if (manager == null)
            {
                return NotFound();
            }
            Company company = await _companyService.GetCompanyById(manager.CompanyId);
            IEnumerable<Employee> employees = company.Employees;
            foreach (var emp in employees)
            {
                await _userManager.SetLockoutEnabledAsync(emp.ApplicationUser, true);
                await _userManager.SetLockoutEndDateAsync(emp.ApplicationUser, Convert.ToDateTime("01/01/4000"));
                //await _userManager.SetLockoutEndDateAsync(emp.ApplicationUser, DateTime.Now);
            }
            _companyService.Deactivate(company.Id);
            await _userManager.SetLockoutEnabledAsync(manager.ApplicationUser, true);
            await _userManager.SetLockoutEndDateAsync(manager.ApplicationUser, Convert.ToDateTime("01/01/4000"));

            return Ok();
        }

    }
}
