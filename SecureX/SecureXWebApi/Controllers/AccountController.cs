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
    public class AccountController : Controller
    {
        private readonly SecureXRepository _Repo;

        public AccountController(SecureXRepository Repo)
        {
            _Repo = Repo;
        }
        
        // GET: api/<controller>
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {            
                var accountlist = await _Repo.GetAccounts();
                return Ok(accountlist);
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
                var account = await _Repo.GetAccountById(x);
                return Ok(account);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Create(Account account)
        {
            _Repo.AddAccount(account);
            _Repo.Save();

            return CreatedAtRoute("Get Account", new { id = account.Id }, account);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string type , Account account)
        {
            Account selectAcc = await _Repo.GetAccountById(account.Id);
            

            if (selectAcc == null)
            {
                return NotFound();
            }

            selectAcc.AccountType = type;
            selectAcc.AccountType = account.AccountType;
            _Repo.Save();

            return NoContent();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Account selectAcc = await _Repo.GetAccountById(id);
            if(selectAcc == null)
            {
                return NotFound();
            }

            _Repo.DeleteAccount(id);
            _Repo.Save();

            return NoContent();
        }
    }
}
