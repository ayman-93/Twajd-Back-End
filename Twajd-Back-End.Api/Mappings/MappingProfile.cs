using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twajd_Back_End.Api.Resources;
using Twajd_Back_End.Core.Models;
using Twajd_Back_End.Core.Models.Auth;

namespace Twajd_Back_End.Api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Owner
            CreateMap<AddOwnerResource, ApplicationUser>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(ur => ur.Email.ToLowerInvariant()));

            CreateMap<ApplicationUser, OwnerResource>();
            
            //Manager
            CreateMap<AddMangerResource, ApplicationUser>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(ur => ur.Email.ToLowerInvariant()));

            CreateMap<AddMangerResource, Manager>();
            //    .ForMember(manager => manager.FullName, opt => opt.MapFrom(managerResource => managerResource.MangerFullName));

            CreateMap<AddMangerResource, Company>()
                .ForMember(comp => comp.Name, opt => opt.MapFrom(managerResource => managerResource.CompanyName));

            CreateMap<Manager, ManagerResource>()
                .ForMember(mngrRes => mngrRes.CompanyName, opt => opt.MapFrom(mngr => mngr.Company.Name))
                .ForMember(mngrRes => mngrRes.PackageType, opt => opt.MapFrom(mngr => mngr.Company.PackageType))
                .ForMember(mngrRes => mngrRes.NumberOfEmployees, opt => opt.MapFrom(mngr => mngr.Company.NumberOfEmployees))
                .ForMember(mngrRes => mngrRes.Email, opt => opt.MapFrom(mngr => mngr.ApplicationUser.Email))
                .ForMember(mngrRes => mngrRes.PhoneNumber, opt => opt.MapFrom(mngr => mngr.ApplicationUser.PhoneNumber));


            // Employee
            CreateMap<AddEmployeeResource, ApplicationUser>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(ur => ur.Email.ToLowerInvariant()));

            CreateMap<AddEmployeeResource, Employee>();

            CreateMap<Employee, EmployeeResource>()
                .ForMember(empRes => empRes.CompanyName, opt => opt.MapFrom(emp => emp.Company.Name))
                //.ForMember(empRes => empRes.Email, opt => opt.MapFrom(emp => emp.ApplicationUser.Email))
                .ForMember(empRes => empRes.PhoneNumber, opt => opt.MapFrom(emp => emp.ApplicationUser.PhoneNumber));

            CreateMap<EmployeeResource, Employee>();
        }
    }
}
