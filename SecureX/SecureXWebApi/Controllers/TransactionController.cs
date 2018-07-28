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
    public class TransactionController : Controller
    {
        private readonly SecureXRepository _Repo;

        public TransactionController(SecureXRepository Repo)
        {
            _Repo = Repo;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var tranlist = await _Repo.GetTransactions();
            return Ok(tranlist);
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
                var tran = await _Repo.GetTransactionById(x);
                return Ok(tran);
            }
            catch (DbUpdateException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Create(Transaction tran)
        {
            await _Repo.AddTransaction(tran);
            await _Repo.Save();

            return NoContent();
        }
    }
}
