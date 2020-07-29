using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Twajd_Back_End.Core.Services;
using Twajd_Back_End.Core.Models;

namespace Twajd_Back_End.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("a/{id}")]
        public async Task<Employee> GetEmployeeById(Guid id)
        {
            return await _employeeService.GetEmployeeById(id);
        }

        // POST: api/Companies
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            return await _employeeService.AddEmployee(employee);
        }
    }
}
