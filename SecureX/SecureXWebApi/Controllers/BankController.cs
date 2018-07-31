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
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BankController : Controller
    {
        private readonly ISecureXRepository IRepo;

        public BankController(ISecureXRepository Repo)
        {
            IRepo = Repo;
        }

        // GET: api/<controller>
        //ELA test
       [HttpGet]
       public async Task<IEnumerable<Bank>> GetAll()
       {
            var banklist = await IRepo.GetBanks();
            return banklist;
       }

        // GET api/<controller>/5
        //ELA tested
        [HttpGet("{id}")]
        [FormatFilter]
        public async Task<ActionResult<Bank>> GetById(int id)
        {
            if(!ModelState.IsValid)
            {
                return NotFound();
            }
            try
            {
                var bank = await IRepo.GetBankById(id);
                return bank;
            }
            catch(DbUpdateException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] Bank bank)
        {
            Bank selectBank = await IRepo.GetBankById(bank.Id);

            if(selectBank == null)
            {
                return NotFound();
            }
            selectBank.Reserves = bank.Reserves;
            await IRepo.UpdateBank(selectBank);
            await IRepo.Save();

            return NoContent();
        }

        
    }
}
