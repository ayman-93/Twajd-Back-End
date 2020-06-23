using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Twajd_Back_End.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        // GET: admin/test
        [SwaggerOperation(Tags = new[] { "Admin-Test" })]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: admin/test/5
        [SwaggerOperation(Tags = new[] { "Admin-Test" })]
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
    }
}