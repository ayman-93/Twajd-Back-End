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
    public class ManagerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IManagerService _managerService;
        private readonly IEmployeeService _employeeService;

        public ManagerController(IMapper mapper, UserManager<ApplicationUser> userManager
            , IEmployeeService employeeService, IManagerService managerService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _managerService = managerService;
            _employeeService = employeeService;
        }

        //add employee
        [HttpPost("add-employee")]
        [Authorize(Roles = Role.Manager)]
        public async Task<ActionResult<Employee>> CreateEmployee(AddEmployeeResource addEmployeeResource)
        {
            Guid managerApplicationUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            Manager manager = await _managerService.GetManagerByApplicationUserId(managerApplicationUserId);
            
            ApplicationUser employeeApplicationUser = _mapper.Map<AddEmployeeResource, ApplicationUser>(addEmployeeResource);

            var userCreateResult = await _userManager.CreateAsync(employeeApplicationUser, addEmployeeResource.Password);

            if (userCreateResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(employeeApplicationUser, Role.Employee);
                Employee employee = _mapper.Map<AddEmployeeResource, Employee>(addEmployeeResource);
                employee.ApplicationUserId = employeeApplicationUser.Id;
                employee.CompanyId = manager.CompanyId;
                return await _employeeService.AddEmployee(employee);
            }

            return Problem(userCreateResult.Errors.First().Description, null, 500);
        }

        [HttpGet("Employees")]
        [Authorize(Roles = Role.Owner + ", " + Role.Manager)]
        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            Guid managerApplicationUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            Manager manager = await _managerService.GetManagerByApplicationUserId(managerApplicationUserId);
            return await _employeeService.GetEmployeesByCompanyId(manager.CompanyId);
        }

        [HttpGet("Employee/{id}")]
        [Authorize(Roles = Role.Owner +", "+ Role.Manager)]
        public async Task<Employee> GetEmployeeById(Guid id)
        {
            return await _employeeService.GetEmployeeById(id);
        }
    }
}
