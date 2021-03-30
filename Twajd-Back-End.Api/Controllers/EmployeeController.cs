using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Twajd_Back_End.Core.Services;
using Twajd_Back_End.Core.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Twajd_Back_End.Core.Models.Auth;
using Twajd_Back_End.Api.Resources;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace Twajd_Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IManagerService _managerService;
        private readonly IEmployeeService _employeeService;
        private readonly ICompanyService _companyService;

        public EmployeeController(
            IMapper mapper,
            UserManager<ApplicationUser> userManager,
            IManagerService managerService,
            IEmployeeService employeeService,
            ICompanyService companyService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _managerService = managerService;
            _employeeService = employeeService;
            _companyService = companyService;
        }




        /// <summary>
        /// Add a new employee, used by manager
        /// </summary>
        /// <param name="addEmployeeResource"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = Role.Manager)]
        public async Task<ActionResult> CreateEmployee(AddEmployeeResource addEmployeeResource)
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
                _companyService.IncrementEmployeeNumber(manager.CompanyId);
                _employeeService.AddEmployee(employee);
                return Ok(new CustomMessge() { Message= String.Format("Employee {0} is created", employee.FullName) });
            }

            return Problem(userCreateResult.Errors.First().Description, null, 400);
        }

        /// <summary>
        /// Add multiple employees, used by manager
        /// </summary>
        /// <param name="addEmployeeResource"></param>
        /// <returns></returns>
        [HttpPost("employees")]
        [Authorize(Roles = Role.Manager)]
        public async Task<ActionResult> CreateEmployees(AddEmployeeResource[] addEmployeeResource)
        {
            Guid managerApplicationUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            Manager manager = await _managerService.GetManagerByApplicationUserId(managerApplicationUserId);

            var employeesApplicationUser = _mapper.Map<AddEmployeeResource[], ApplicationUser[]>(addEmployeeResource);

            List<Employee> employees = new List<Employee>();
            List<string> errors = new List<string>();
            // if something go wrong delete this users
            List<ApplicationUser> employeesToDelete = new List<ApplicationUser>();

            for (var i = 0; i < addEmployeeResource.Length; i++)
            {
                var userCreateResult = await _userManager.CreateAsync(employeesApplicationUser[i], addEmployeeResource[i].Password);
                Employee employee = _mapper.Map<AddEmployeeResource, Employee>(addEmployeeResource[i]);
                if (userCreateResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(employeesApplicationUser[i], Role.Employee);
                    employee.ApplicationUserId = employeesApplicationUser[i].Id;
                    employee.CompanyId = manager.CompanyId;
                    _companyService.IncrementEmployeeNumber(manager.CompanyId);
                    employees.Add(employee);
                    employeesToDelete.Add(employeesApplicationUser[i]);
                    //_employeeService.AddEmployee(employee);
                    //return Ok(201);
                }
                else
                {

                    errors.Add(employee.FullName + ": " + userCreateResult.Errors.First().Description);
                }
            }
            // if there is no errors add the employees
            if (!errors.Any())
            {
                _employeeService.AddEmployees(employees.ToArray());
                return StatusCode(201);
            }
            // else remove the employees from ApplicationUser
            else
            {
                foreach (ApplicationUser emp in employeesToDelete)
                {
                    _companyService.DecrementEmployeeNumber(manager.CompanyId);
                    await _userManager.DeleteAsync(emp);
                }
                return BadRequest(errors);
            }

        }

        /// <summary>
        /// Get all employees, used by manager.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = Role.Manager)]
        public async Task<ActionResult<IEnumerable<EmployeeResource>>> GetEmployees()
        {
            Guid managerApplicationUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Manager manager = await _managerService.GetManagerByApplicationUserId(managerApplicationUserId);
            Company company = await _companyService.GetCompanyById(manager.CompanyId);
            var employeeResources = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeResource>>(company.Employees);
            return Ok(employeeResources);
        }

        /// <summary>
        /// get an employee by id, used by manager.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(Roles = Role.Manager)]
        public async Task<ActionResult<EmployeeResource>> GetEmployee(Guid id)
        {
            Guid managerApplicationUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Manager manager = await _managerService.GetManagerByApplicationUserId(managerApplicationUserId);
            Employee employee = await _employeeService.GetById(id);
            if (employee == null && employee.CompanyId != manager.CompanyId)
            {
                return NotFound();
            }

            EmployeeResource employeeResource = _mapper.Map<Employee, EmployeeResource>(employee);
            return Ok(employeeResource);
        }

        /// <summary>
        /// Update an employee profile, used by employee.
        /// </summary>
        /// <param name="employeeResource"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Roles = Role.Employee)]
        public async Task<ActionResult<EmployeeResource>> UpdateEmployee(EmployeeResource employeeResource)
        {
            Guid employeeApplicationUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Employee employee = await _employeeService.GetEmployeeByApplicationUserId(employeeApplicationUserId);
            Guid employeeId = employee.Id;
            Employee employeeToUpdate = _mapper.Map<EmployeeResource, Employee>(employeeResource, employee);
            if (employee == null || employeeId != employeeResource.Id)
            {
                return Unauthorized("You can't change others data");
            }

            _employeeService.Update(employeeToUpdate);
            Employee updatedEmployee = await _employeeService.GetById(employee.Id);
            EmployeeResource updatedEmployeeResource = _mapper.Map<Employee, EmployeeResource>(updatedEmployee);
            return Ok(updatedEmployeeResource);


        }

        /// <summary>
        /// Deactivate one employee by id, used by manager
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = Role.Manager)]
        public async Task<ActionResult> DeleteEmployee(Guid id)
        {
            Guid managerApplicationUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            Manager manager = await _managerService.GetManagerByApplicationUserId(managerApplicationUserId);

            Employee employee = await _employeeService.GetById(id);
            if (employee == null && employee.CompanyId != manager.CompanyId)
            {
                return NotFound();
            }
            _companyService.DecrementEmployeeNumber(employee.CompanyId);
            //await _userManager.DeleteAsync(employee.ApplicationUser);
            await _userManager.SetLockoutEnabledAsync(employee.ApplicationUser, true);
            await _userManager.SetLockoutEndDateAsync(employee.ApplicationUser, Convert.ToDateTime("01/01/4000"));
            //_employeeService.DeleteEmployeeById(id);
            return Ok();
        }

        /// <summary>
        /// Deactivate multiple employees, used by manager
        /// </summary>
        /// <param name="employeesIds"></param>
        /// <returns></returns>
        [HttpDelete("employees")]
        [Authorize(Roles = Role.Manager)]
        public async Task<ActionResult> DeleteEmployees(Guid[] employeesIds)
        {
            Guid managerApplicationUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            Manager manager = await _managerService.GetManagerByApplicationUserId(managerApplicationUserId);

            DeleteEmployeesResult result = new DeleteEmployeesResult();

            foreach (Guid id in employeesIds)
            {
                Employee employee = await _employeeService.GetById(id);
                if (employee == null && employee.CompanyId != manager.CompanyId)
                {
                    result.UnfoundUsers.Add(id);
                }
                else
                {
                    _companyService.DecrementEmployeeNumber(employee.CompanyId);
                    //await _userManager.DeleteAsync(employee.ApplicationUser);
                    await _userManager.SetLockoutEnabledAsync(employee.ApplicationUser, true);
                    await _userManager.SetLockoutEndDateAsync(employee.ApplicationUser, Convert.ToDateTime("01/01/4000"));
                    result.DeletedUsers.Add(id);
                }
            }
            return Ok(result);
        }

    }
}
