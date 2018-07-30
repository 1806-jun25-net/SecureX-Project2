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
    //[Authorize]
    public class BankController : Controller
    {
        private readonly ISecureXRepository IRepo;

        public BankController(ISecureXRepository Repo)
        {
            IRepo = Repo;
        }

        // GET: api/<controller>
       [HttpGet]
       public async Task<ActionResult> GetAll()
       {
            var banklist = await IRepo.GetBanks();
            return Ok(banklist);
       }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [FormatFilter]
        public async Task<ActionResult<Bank>> GetById(int x)
        {
            if(!ModelState.IsValid)
            {
                return NotFound();
            }
            try
            {
                var bank = await IRepo.GetBankById(x);
                return bank;
            }
            catch(DbUpdateException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Bank bank)
        {
            bank.Id = 0;
            await IRepo.AddBank(bank);
            await IRepo.Save();

            return NoContent();
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
