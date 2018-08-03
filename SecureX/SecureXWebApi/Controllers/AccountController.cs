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
    public class AccountController : Controller
    {
        private readonly ISecureXRepository IRepo;

        public AccountController(ISecureXRepository Repo)
        {
            IRepo = Repo;
        }

        // GET: api/<controller>
        //ELA tested
        [HttpGet]
        public async Task<IEnumerable<Account>> GetAll()
        {            
                var accountlist = await IRepo.GetAccounts();
                return accountlist;
        }

        // GET api/<controller>/5
        //ELA tested
        [HttpGet("{id}")]
        [FormatFilter]
        public async Task<ActionResult<Account>> GetById(int id)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            try
            {
                var account = await IRepo.GetAccountById(id);
                return account;
            }
            catch (DbUpdateException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Account account)
        {
            account.Id = 0;
            await IRepo.AddAccount(account);
            await IRepo.Save();

            return NoContent();
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] Account account)
        {
            Account selectAcc = await IRepo.GetAccountById(account.Id);
            

            if (selectAcc == null)
            {
                return NotFound();
            }

            selectAcc.AccountType = account.AccountType;
            selectAcc.Funds = account.Funds;
            await IRepo.UpdateAccount(selectAcc);
            await IRepo.Save();

            return NoContent();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            Account selectAcc = await IRepo.GetAccountById(id);
            if(selectAcc == null)
            {
                return NotFound();
            }

            await IRepo.DeleteAccount(id);
            await IRepo.Save();

            return NoContent();
        }
    }
}
