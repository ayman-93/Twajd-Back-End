using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Twajd_Back_End.Business.Services;
using Twajd_Back_End.Core.Models;

namespace Twajd_Back_End.Areas.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("{id}")]
        public async Task<Employee> GetEmployeeById(Guid id)
        {
            return await _employeeService.GetEmployeeById(id);
        }
    }
}
