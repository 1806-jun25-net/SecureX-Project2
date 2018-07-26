using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureXLibrary;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SecureXWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : Controller
    {
        private readonly SecureXRepository _Repo;

        public BankController(SecureXRepository Repo)
        {
            _Repo = Repo;
        }

        // GET: api/<controller>
        [HttpGet]
       public async Task<ActionResult> GetAll()
       {
            var banklist = await _Repo.GetBanks();
            return Ok(banklist);
       }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bank>> GetById(int x)
        {
            if(!ModelState.IsValid)
            {
                return NotFound();
            }
            try
            {
                var bank = await _Repo.GetBankById(x);
                return Ok(bank);
            }
            catch(DbUpdateException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Create(Bank bank)
        {
            _Repo.AddBank(bank);
            _Repo.Save();

            return CreatedAtRoute("Get Bank", new { id = bank.Id }, bank);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(decimal amt, Bank bank)
        {
            Bank selectBank = await _Repo.GetBankById(bank.Id);

            if(selectBank == null)
            {
                return NotFound();
            }

            selectBank.Reserves = amt;
            selectBank.Reserves = bank.Reserves;
            _Repo.Save();

            return NoContent();
        }

        
    }
}
