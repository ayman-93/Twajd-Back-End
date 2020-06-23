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
    public class EmployeeController : ControllerBase
    {
        // GET: Manger/Employee
        [SwaggerOperation(Tags = new[] { "Manger-Employee" })]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: Manger/Employee/5
        [SwaggerOperation(Tags = new[] { "Manger-Employee" })]
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: Manger/Employee
        [SwaggerOperation(Tags = new[] { "Manger-Employee" })]
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: Manger/Employee/5
        [SwaggerOperation(Tags = new[] { "Manger-Employee" })]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: Manger/ApiWithActions/5
        [SwaggerOperation(Tags = new[] { "Manger-Employee" })]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
