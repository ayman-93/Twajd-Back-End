using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Twajd_Back_End.Api.Resources;
using Twajd_Back_End.Core.Models.Auth;

namespace Twajd_Back_End.Api.Controllers
{
    [Route("twajd-api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        public OwnerController(IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        /// <summary>
        ///  new Owner, used by owner
        /// </summary>
        /// <param name="addOwnerResource"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = Role.Owner)]
        public async Task<ActionResult> CreateOwner(AddOwnerResource addOwnerResource)
        {
            ApplicationUser user = _mapper.Map<AddOwnerResource, ApplicationUser>(addOwnerResource);

            var userCreateResult = await _userManager.CreateAsync(user, addOwnerResource.Password);
            if (userCreateResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Role.Owner);
                return Ok(201);
            }

            return Problem(userCreateResult.Errors.First().Description, null, 500);
        }

        /// <summary>
        /// Delete owner, used by another owner.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = Role.Owner)]
        public async Task<ActionResult> DeleteOwner(Guid id)
        {
            ApplicationUser logedInOwner = await _userManager.GetUserAsync(this.User);
            if (logedInOwner.Id == id)
            {
                return BadRequest("you can't delete your self");
            }
            ApplicationUser ownerToDelete = await _userManager.FindByIdAsync(id.ToString());
            await _userManager.DeleteAsync(ownerToDelete);
            return Ok(201);

        }

    }
}
