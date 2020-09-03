using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
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
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManger;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private readonly IManagerService _managerService;
        private readonly IEmployeeService _employeeService;
        private IHttpContextAccessor _httpContextAccessor;
        public UserController(
            IAuthService authService,
            IMapper mapper,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IManagerService managerService,
            IEmployeeService employeeService,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManger = signInManager;
            _authService = authService;
            _managerService = managerService;
            _employeeService = employeeService;
            _httpContextAccessor = httpContextAccessor;
        }


        /// <summary>
        /// Login users, used by owner, manager or employee
        /// </summary>
        /// <param name="userLoginResource"></param>
        /// <returns></returns>
        [HttpPost("token")]
        public async Task<ActionResult> Login(UserLoginResource userLoginResource)
        {
            ApplicationUser user = _userManager.Users.SingleOrDefault(u => u.UserName == userLoginResource.Email.ToLowerInvariant());
            if (user is null)
            {
                return NotFound("User not found");
            }
            var userSigninResult = await _signInManger.CheckPasswordSignInAsync(user, userLoginResource.Password, true);

            if (userSigninResult.IsLockedOut)
            {
                return BadRequest("you are locked out");
            }

            if (userSigninResult.Succeeded)
            {

                var roles = await _userManager.GetRolesAsync(user);
                var token = _authService.GenerateJwt(user, roles);

                if (roles.Contains("Owner"))
                {
                    OwnerResource owner = _mapper.Map<ApplicationUser, OwnerResource>(user);
                    owner.Token = token;
                    return Ok(owner);
                }
                else if (roles.Contains("Manager"))
                {
                    Manager managerEntity = await _managerService.GetManagerByApplicationUserId(user.Id);
                    ManagerResource manager = _mapper.Map<Manager, ManagerResource>(managerEntity);
                    manager.Token = token;
                    return Ok(manager);
                }
                else if (roles.Contains("Employee"))
                {
                    Employee employeeEntity = await _employeeService.GetEmployeeByApplicationUserId(user.Id);
                    EmployeeResource employee = _mapper.Map<Employee, EmployeeResource>(employeeEntity);
                    employee.Token = token;
                    return Ok(employee);
                }
            }

            return BadRequest("Email or password incorrect.");
        }

        /// <summary>
        /// Change password, used by owner, manager or employee.
        /// </summary>
        /// <param name="changePasswordResource"></param>
        /// <returns></returns>
        [HttpPut("password")]
        public async Task<ActionResult> ChangePassword(ChangePasswordResource changePasswordResource)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.ChangePasswordAsync(user, changePasswordResource.OldPassword, changePasswordResource.NewPassword);
            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        /// <summary>
        /// invalidate user token.
        /// </summary>
        /// <returns></returns>
        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            await _authService.DeactivateCurrentAsync();
            return Ok();
        }

        
        
    }
}
