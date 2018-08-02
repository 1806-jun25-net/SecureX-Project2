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
    public class CustomerController : Controller
    {
        private readonly ISecureXRepository IRepo;

        public CustomerController(ISecureXRepository Repo)
        {
            IRepo = Repo;
        }

        // GET: api/<controller>
        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<Customer>> GetAll()
        {
            var customerlist = await IRepo.GetCustomers();
            return customerlist;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [FormatFilter]
        public async Task<ActionResult<Customer>> GetById(int x)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            try
            {
                var customer = await IRepo.GetCustomerById(x);
                return customer;
            }
            catch (DbUpdateException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{Bank}")]
        [FormatFilter]
        public async Task<ActionResult<List<Customer>>> GetCustomerByBank(Bank Bank)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            try
            {
                var cust = await IRepo.GetCustomerByLocation(Bank);
                return cust;
            }
            catch (DbUpdateException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST api/<controller>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Customer customer)
        {
            customer.Id = 0;
            await IRepo.AddCustomer(customer);
            await IRepo.Save();

            return NoContent();
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] Customer customer)
        {
            Customer selectcust = await IRepo.GetCustomerById(customer.Id);


            if (selectcust == null)
            {
                return NotFound();
            }
            selectcust.PhoneNumber = customer.PhoneNumber;
            selectcust.Address = customer.Address;
            selectcust.City = customer.City;
            await IRepo.UpdateCustomer(selectcust);
            await IRepo.Save();

            return NoContent();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Customer selectcust = await IRepo.GetCustomerById(id);
            if (selectcust == null)
            {
                return NotFound();
            }

            await IRepo.DeleteCustomer(id);
            await IRepo.Save();

            return NoContent();
        }
    }
}
