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
        private readonly ISecureXRepository IRepo;

        public TransactionController(ISecureXRepository Repo)
        {
            IRepo = Repo;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var tranlist = await IRepo.GetTransactions();
            return Ok(tranlist);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [FormatFilter]
        public async Task<ActionResult<Transaction>> GetById(int x)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            try
            {
                var tran = await IRepo.GetTransactionById(x);
                return tran;
            }
            catch (DbUpdateException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Transaction tran)
        {
            tran.Id = 0;
            await IRepo.AddTransaction(tran);
            await IRepo.Save();

            return NoContent();
        }
    }
}
