using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Twajd_Back_End.Controllers
{
    //[Area("Admin")]
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        // GET: admin/User
        //[SwaggerOperation(Tags = new[] { "Admin-User" })]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// This Function Return Specific User By Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: admin/User/5
        //[SwaggerOperation(Tags = new[] { "Admin-User" })]
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: admin/User
        //[SwaggerOperation(Tags = new[] { "Admin-User" })]
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: admin/User/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        //[SwaggerOperation(Tags = new[] { "Admin-User" })]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: admin/ApiWithActions/5
        //[SwaggerOperation(Tags = new[] { "Admin-User" })]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
