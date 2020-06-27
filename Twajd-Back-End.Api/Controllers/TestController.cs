using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Twajd_Back_End.Controllers
{
    //[Area("Admin")]
    [Route("[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        // GET: admin/test
        //[SwaggerOperation(Tags = new[] { "Admin-Test" })]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: admin/test/5
       // [SwaggerOperation(Tags = new[] { "Admin-Test" })]
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
    }
}