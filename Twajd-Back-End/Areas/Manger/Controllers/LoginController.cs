using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Twajd_Back_End.Areas.Manger.Controllers
{
    [Area("Manger")]
    [Route("[area]/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        // GET: Manger/Login
        [SwaggerOperation(Tags = new[] { "Manger-Login" })]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: Manger/Login/5
        [SwaggerOperation(Tags = new[] { "Manger-Login" })]
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: Manger/Login
        [SwaggerOperation(Tags = new[] { "Manger-Login" })]
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: Manger/Login/5
        [SwaggerOperation(Tags = new[] { "Manger-Login" })]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: Manger/ApiWithActions/5
        [SwaggerOperation(Tags = new[] { "Manger-Login" })]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
