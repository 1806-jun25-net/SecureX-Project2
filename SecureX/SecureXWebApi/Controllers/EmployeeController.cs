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
    //[Authorize]
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly SecureXRepository _Repo;

        public EmployeeController(SecureXRepository Repo)
        {
            _Repo = Repo;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var employlist = await _Repo.GetEmployees();
            return Ok(employlist);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetById(int x)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            try
            {
                var employ = await _Repo.GetEmployeeById(x);
                return Ok(employ);
            }
            catch (DbUpdateException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            await _Repo.AddEmployee(employee);
            await _Repo.Save();

            return NoContent();
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int bank, Employee employee)
        {
            Employee selectemploy = await _Repo.GetEmployeeById(employee.Id);


            if (selectemploy == null)
            {
                return NotFound();
            }

            selectemploy.BankId = bank;
            selectemploy.BankId = employee.BankId;
            await _Repo.Save();

            return NoContent();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Employee selectemploy = await _Repo.GetEmployeeById(id);
            if (selectemploy == null)
            {
                return NotFound();
            }

            await _Repo.DeleteEmployee(id);
            await _Repo.Save();

            return NoContent();
        }
    }
}
