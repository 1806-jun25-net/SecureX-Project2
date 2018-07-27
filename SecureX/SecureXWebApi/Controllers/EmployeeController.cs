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
            catch (DbUpdateException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            _Repo.AddEmployee(employee);
            _Repo.Save();

            return CreatedAtRoute("Get Employee", new { id = employee.Id }, employee);
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
            _Repo.Save();

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

            _Repo.DeleteEmployee(id);
            _Repo.Save();

            return NoContent();
        }
    }
}
