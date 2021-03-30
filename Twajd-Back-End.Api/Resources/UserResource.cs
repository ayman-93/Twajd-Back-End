
using System.ComponentModel.DataAnnotations;

namespace Twajd_Back_End.Api.Resources
{
    public class UserLoginResource
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsMobile { get; set; }
    }
    public class ChangePasswordResource
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        [Compare("NewPassword")]
        public string ConfirmNewPassword { get; set; }
    }
    public class ResetPasswordResource
    {
        public string Email { get; set; }
        public string RestPasswordToken { get; set; }
        public string NewPassword { get; set; }
        [Compare("NewPassword")]
        public string ConfirmNewPassword { get; set; }
    }
    public class ResetPasswordTokenResource
    {
        public string ResetPasswordToken { get; set; }
    }
}
