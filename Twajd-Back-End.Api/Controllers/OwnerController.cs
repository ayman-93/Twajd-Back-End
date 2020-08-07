using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class OwnerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOwnerService _ownerService;
        private readonly ICompanyService _companyService;
        public OwnerController(IMapper mapper, UserManager<ApplicationUser> userManager, ICompanyService companyService
            , IOwnerService ownerService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _companyService = companyService;
            _ownerService = ownerService;
        }

        // to be commented
        [HttpPost("add-Owner")]
        //[Authorize(Roles = Role.Owner)]
        public async Task<ActionResult<ApplicationUser>> CreateManager(AddOwnerResource userSignUpResource)
        {
            ApplicationUser user = _mapper.Map<AddOwnerResource, ApplicationUser>(userSignUpResource);

            var userCreateResult = await _userManager.CreateAsync(user, userSignUpResource.Password);
            if (userCreateResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Role.Owner);
                return Created(string.Empty, string.Empty);
            }

            return Problem(userCreateResult.Errors.First().Description, null, 500);
        }

        // Manager
        [HttpPost("add-Manager")]
        [Authorize(Roles = Role.Owner)]
        public async Task<ActionResult<Manager>> CreateManager(AddMangerResource mangerResource)
        {
            ApplicationUser user = _mapper.Map<AddMangerResource, ApplicationUser>(mangerResource);
            Company company = _mapper.Map<AddMangerResource, Company>(mangerResource);
            Manager manager = _mapper.Map<AddMangerResource, Manager>(mangerResource);

            var userCreateResult = await _userManager.CreateAsync(user, mangerResource.Password);
            if (userCreateResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Role.Manager);
                await _companyService.AddCompany(company);
                return await _ownerService.addManagerAndCompany(manager, user.Id, company.Id);
                //return Created(string.Empty, string.Empty);
            }

            return Problem(userCreateResult.Errors.First().Description, null, 500);
        }
    

    }
}
