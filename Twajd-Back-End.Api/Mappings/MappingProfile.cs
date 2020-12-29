using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twajd_Back_End.Api.Resources;
using Twajd_Back_End.Core.Helper;
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
                .ForMember(empRes => empRes.PhoneNumber, opt => opt.MapFrom(emp => emp.ApplicationUser.PhoneNumber))
                .ForMember(empRes => empRes.LocationName, opt => opt.MapFrom(emp => emp.Location.Name))
                .ForMember(empRes => empRes.WorkHoursName, opt => opt.MapFrom(emp => emp.WorkHours.Name));

            CreateMap<EmployeeResource, Employee>();

            // WorkHours
            CreateMap<AddWorkHoursDayResource, WorkHoursDay>()
                .ForMember(whd => whd.StartWork, opt => opt.MapFrom(whdr => Helpers.strToTimeSpan(whdr.StartWork)))
                .ForMember(whd => whd.EndWork, opt => opt.MapFrom(whdr => Helpers.strToTimeSpan(whdr.EndWork)))
                .ForMember(whd => whd.FlexibleHour, opt => opt.MapFrom(whdr => TimeSpan.FromHours(whdr.FlexibleHour)));
            //.ForMember(wh => wh.StartWork, opt => opt.ConvertUsing<StringToTimeSpanConverter>())
            //(whr => StringToTimeSpanConverter(whr.StartWork));

            CreateMap<AddWorkHoursResource, WorkHours>();
            CreateMap<WorkHours, AddWorkHoursResource>();


            CreateMap<WorkHoursDay, WorkHoursDayResource>()
                .ForMember(whdr => whdr.Day, opt => opt.MapFrom(whd => Enum.GetName(typeof(DayOfWeek), whd.Day)))
                .ForMember(whdr => whdr.StartWork, opt => opt.MapFrom(whd => Helpers.TimeSpanToStr(whd.StartWork)))
                .ForMember(whdr => whdr.EndWork, opt => opt.MapFrom(whd => Helpers.TimeSpanToStr(whd.EndWork)))
                .ForMember(whdr => whdr.FlexibleHour, opt => opt.MapFrom(whd => whd.FlexibleHour.TotalHours));

            CreateMap<WorkHours, WorkHoursResource>();

            // Locations
            CreateMap<Location, LocationResource>();
            CreateMap<AddLocationResource, Location>();
            
        }
    }
}
