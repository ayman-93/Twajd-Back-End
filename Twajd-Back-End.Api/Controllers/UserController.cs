using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Twajd_Back_End.Api.Resources;
using Twajd_Back_End.Core.Models;
using Twajd_Back_End.Core.Models.Auth;
using Twajd_Back_End.Core.Services;


namespace Twajd_Back_End.Api.Controllers
{
    class identityErrors
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManger;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private readonly IManagerService _managerService;
        private readonly IEmployeeService _employeeService;
        private readonly IMailer _mailer;
        private IHttpContextAccessor _httpContextAccessor;
        public UserController(
            IAuthService authService,
            IMapper mapper,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IManagerService managerService,
            IEmployeeService employeeService,
            IMailer mailer,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManger = signInManager;
            _authService = authService;
            _managerService = managerService;
            _employeeService = employeeService;
            _mailer = mailer;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Get logged in user profile
        /// </summary>
        /// <returns></returns>
        [HttpGet("profile")]
        public async Task<ActionResult> Profile()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest(new CustomMessge() { Message = "You are not loged in, try to login again" });
            }
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Contains("Manager"))
            {
                Manager managerEntity = await _managerService.GetManagerByApplicationUserId(user.Id);
                ManagerResource manager = _mapper.Map<Manager, ManagerResource>(managerEntity);
                return Ok(manager);
            }
            else if (roles.Contains("Employee"))
            {
                //if (version < 1.0)
                //{
                //    return BadRequest(new CustomMessge() { Message = "You Can't login, Please update the application" });
                //}
                Employee employeeEntity = await _employeeService.GetEmployeeByApplicationUserId(user.Id);
                EmployeeResource employee = _mapper.Map<Employee, EmployeeResource>(employeeEntity);
                return Ok(employee);
            }
            else
            {
                return BadRequest(new CustomMessge() { Message = "Sorry no profile for you." });
            }
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
                return NotFound(new CustomMessge() { Message = "User not found" });
            }
            var userSigninResult = await _signInManger.CheckPasswordSignInAsync(user, userLoginResource.Password, true);

            if (userSigninResult.IsLockedOut)
            {
                return BadRequest(new CustomMessge() { Message = "you are locked out" });
            }

            if (userSigninResult.Succeeded)
            {

                var roles = await _userManager.GetRolesAsync(user);
                var token = _authService.GenerateJwt(user, roles);

                if (roles.Contains("Owner"))
                {
                    if (userLoginResource.IsMobile)
                    {
                        return BadRequest(new CustomMessge() { Message = "You Can't login from mobile" });
                    }
                    OwnerResource owner = _mapper.Map<ApplicationUser, OwnerResource>(user);
                    owner.Token = token;
                    return Ok(owner);
                }
                else if (roles.Contains("Manager"))
                {
                    if (userLoginResource.IsMobile)
                    {
                        return BadRequest(new CustomMessge() { Message = "You Can't login from mobile" });
                    }
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

            return BadRequest(new CustomMessge() { Message = "Email or password incorrect." });
        }

        /// <summary>
        /// Request otp key to reset password it will send the otp key to email, step 1.
        /// </summary>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        [HttpGet("forgetPassword")]
        public async Task<IActionResult> PasswordResetToken(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            if(user == null)
            {
                return BadRequest(new CustomMessge() { Message = "Email not exist" });
            }

            var reseToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var otpKey = new Random().Next(100000, 1000000).ToString();
            await _authService.setEmailOtpToken(otpKey, reseToken);

            //string url = string.Format("{0}://{1}/api/User/resetPassword?{2}", _httpContextAccessor.HttpContext.Request.Scheme, _httpContextAccessor.HttpContext.Request.Host.Value, resetParams);
            

            string msg = string.Format("please use this key {0} to reset the password.", otpKey);
            await _mailer.SendEmailAsync("mr.aymoone@gmail.com", "Reset Password", msg);

            return Ok(new CustomMessge() { Message = "A password reset key was sent, valid for 1 day." });
        }

        /// <summary>
        /// request the reset password token by using the otp key, step 2.
        /// </summary>
        /// <param name="otpKey"></param>
        /// <returns></returns>
        [HttpGet("resetPasswordToken")]
        public async Task<IActionResult> ResetPasswordToken(string otpKey)
        {
            string resetPasswordToken = await _authService.getEmialOtpToken(otpKey);
            if (String.IsNullOrEmpty(resetPasswordToken))
            {
                return BadRequest(new CustomMessge() { Message = "Wrong OTP key" });
            }
            return Ok(new ResetPasswordTokenResource(){ ResetPasswordToken = resetPasswordToken });
        }

        /// <summary>
        /// reset password, step 3.
        /// </summary>
        /// <param name="resetPasswordResource"></param>
        /// <returns></returns>
        [HttpPut("resetPassword")]
        public async Task<ActionResult> ResetPassword(ResetPasswordResource resetPasswordResource)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(resetPasswordResource.Email);
            
            var result = await _userManager.ResetPasswordAsync(user, resetPasswordResource.RestPasswordToken, resetPasswordResource.NewPassword);
            if (result.Succeeded)
            {
                return Ok(new CustomMessge() { Message = "Password Changed Successfully" });
            }
            else
            {
                var errors = JsonConvert.SerializeObject(result.Errors);

                errors = "{ \"Errors\":" +
                    errors +
                    "}";
                return BadRequest(errors);
            }
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
            if(user == null)
            {
                return BadRequest(new CustomMessge() { Message = "You are not loged in, try to login again" });
            }
            var result = await _userManager.ChangePasswordAsync(user, changePasswordResource.OldPassword, changePasswordResource.NewPassword);
            if (result.Succeeded)
            {
                return Ok(new CustomMessge() { Message = "Password Changed Successfully" });
            }
            else
            {
                var errors = JsonConvert.SerializeObject(result.Errors);

                errors = "{ \"Errors\":" +
                    errors +
                    "}";
                return BadRequest(errors);
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
            return Ok(new CustomMessge() { Message = "You Are Loged out" });
        }

        
        
    }
}
