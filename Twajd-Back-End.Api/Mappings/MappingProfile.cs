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
            CreateMap<AddOwnerResource, ApplicationUser>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(ur => ur.Email));
            
            //Manager
            CreateMap<AddMangerResource, ApplicationUser>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(ur => ur.Email));

            CreateMap<AddMangerResource, Manager>();
            //    .ForMember(manager => manager.FullName, opt => opt.MapFrom(managerResource => managerResource.MangerFullName));

            CreateMap<AddMangerResource, Company>()
                .ForMember(comp => comp.Name, opt => opt.MapFrom(managerResource => managerResource.CompanyName));


            // Employee
            CreateMap<AddEmployeeResource, ApplicationUser>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(ur => ur.Email));

            CreateMap<AddEmployeeResource, Employee>();

            CreateMap<Employee, EmployeeLoginResponse>();
        }
    }
}
