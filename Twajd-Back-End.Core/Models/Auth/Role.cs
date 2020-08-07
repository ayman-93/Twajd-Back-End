using Microsoft.AspNetCore.Identity;
using System;

namespace Twajd_Back_End.Core.Models.Auth
{
    public class Role : IdentityRole<Guid>
    {
        public const string Owner = "Owner";
        public const string Manager = "Manager";
        public const string Employee = "Employee";
    }
    //public static class Role
    //{
    //    public const string Owner = "Owner";
    //    public const string Manager = "Manager";
    //    public const string Employee = "Employee";
    //}
}
