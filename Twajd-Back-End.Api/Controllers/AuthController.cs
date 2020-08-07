using System;
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
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManger;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private readonly IOwnerService _ownerService;
        private readonly IManagerService _managerService;
        private readonly IEmployeeService _employeeService;
        private readonly ICompanyService _companyService;

        public AuthController(
            IAuthService authService,
            IMapper mapper,
            UserManager<ApplicationUser> userManager,
            RoleManager<Role> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IOwnerService ownerService,
            IManagerService managerService,
            IEmployeeService employeeService,
            ICompanyService companyService
            )
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManger = signInManager;
            _authService = authService;
            _ownerService = ownerService;
            _managerService = managerService;
            _employeeService = employeeService;
            _companyService = companyService;
        }


        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(UserLoginResource userLoginResource)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.UserName == userLoginResource.Email);
            if (user is null)
            {
                return NotFound("User not found");
            }

            var userSigninResult = await _userManager.CheckPasswordAsync(user, userLoginResource.Password);
            var roles = await _userManager.GetRolesAsync(user);
            var token = _authService.GenerateJwt(user, roles);

            if (userSigninResult)
            {
                if (roles.Contains("Owner"))
                {
                return Ok(token);
                }
                else if (roles.Contains("Manager"))
                {
                    var manager = await _managerService.GetManagerByApplicationUserId(user.Id);
                    ManagerLoginResponse managerLoginResponse = new ManagerLoginResponse();
                    managerLoginResponse.FullName = manager.FullName;
                    managerLoginResponse.Token = token;
                    return Ok(managerLoginResponse);
                } else if (roles.Contains("Employee"))
                {
                    Employee employee = await _employeeService.GetEmployeeByApplicationUserId(user.Id);
                    EmployeeLoginResponse emp = _mapper.Map<Employee, EmployeeLoginResponse>(employee);
                    emp.Token = token;
                    return Ok(emp);
                }
            }

            return BadRequest("Email or password incorrect.");
        }

        //[HttpPost("Roles")]
        //public async Task<IActionResult> CreateRole(string roleName)
        //{
        //    if (string.IsNullOrWhiteSpace(roleName))
        //    {
        //        return BadRequest("Role name should be provided.");
        //    }

        //    var newRole = new Role
        //    {
        //        Name = roleName
        //    };

        //    var roleResult = await _roleManager.CreateAsync(newRole);

        //    if (roleResult.Succeeded)
        //    {
        //        return Ok();
        //    }

        //    return Problem(roleResult.Errors.First().Description, null, 500);
        //}

        //[HttpPost("User/{userEmail}/Role")]
        //public async Task<IActionResult> AddUserToRole(string userEmail, [FromBody] RoleNameResource roleName)
        //{
        //    var user = _userManager.Users.SingleOrDefault(u => u.UserName == userEmail);

        //    var result = await _userManager.AddToRoleAsync(user, roleName.Name);

        //    if (result.Succeeded)
        //    {
        //        return Ok();
        //    }

        //    return Problem(result.Errors.First().Description, null, 500);
        //}

    }
}

