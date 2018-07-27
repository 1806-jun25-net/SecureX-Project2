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
    public class CustomerController : Controller
    {
        private readonly SecureXRepository _Repo;

        public CustomerController(SecureXRepository Repo)
        {
            _Repo = Repo;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var customerlist = await _Repo.GetCustomers();
            return Ok(customerlist);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetById(int x)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            try
            {
                var customer = await _Repo.GetCustomerById(x);
                return Ok(customer);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            _Repo.AddCustomer(customer);
            _Repo.Save();

            return CreatedAtRoute("Get Account", new { id = customer.Id }, customer);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int phone, Customer customer)
        {
            Customer selectcust = await _Repo.GetCustomerById(customer.Id);


            if (selectcust == null)
            {
                return NotFound();
            }

            selectcust.PhoneNumber = phone;
            selectcust.PhoneNumber = customer.PhoneNumber;
            _Repo.Save();

            return NoContent();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Customer selectcust = await _Repo.GetCustomerById(id);
            if (selectcust == null)
            {
                return NotFound();
            }

            _Repo.DeleteCustomer(id);
            _Repo.Save();

            return NoContent();
        }
    }
}
