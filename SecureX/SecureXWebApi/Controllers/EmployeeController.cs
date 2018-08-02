using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureXLibrary;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SecureXWebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly ISecureXRepository IRepo;

        public EmployeeController(ISecureXRepository Repo)
        {
            IRepo = Repo;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var employlist = await IRepo.GetEmployees();
            return Ok(employlist);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [FormatFilter]
        public async Task<ActionResult<Employee>> GetById(int x)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            try
            {
                var employ = await IRepo.GetEmployeeById(x);
                return employ;
            }
            catch (DbUpdateException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{Bank}")]
        [FormatFilter]
        public async Task<ActionResult<List<Employee>>> GetEmployeeByBank(Bank Bank)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            try
            {
                var employ = await IRepo.GetEmployeeByLocation(Bank);
                return employ;
            }
            catch (DbUpdateException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Employee employee)
        {
            employee.Id = 0;
            await IRepo.AddEmployee(employee);
            await IRepo.Save();

            return NoContent();
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] Employee employee)
        {
            Employee selectemploy = await IRepo.GetEmployeeById(employee.Id);


            if (selectemploy == null)
            {
                return NotFound();
            }

            selectemploy.BankId = employee.BankId;
            await IRepo.UpdateEmployee(selectemploy);
            await IRepo.Save();

            return NoContent();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Employee selectemploy = await IRepo.GetEmployeeById(id);
            if (selectemploy == null)
            {
                return NotFound();
            }

            await IRepo.DeleteEmployee(id);
            await IRepo.Save();

            return NoContent();
        }
    }
}
