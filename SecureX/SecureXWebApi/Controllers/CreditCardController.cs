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
    public class CreditCardController : Controller
    {
        private readonly SecureXRepository _Repo;

        public CreditCardController(SecureXRepository Repo)
        {
            _Repo = Repo;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var cclist = await _Repo.GetCreditCards();
            return Ok(cclist);
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
                var creditc = await _Repo.GetCreditCardById(x);
                return Ok(creditc);
            }
            catch (DbUpdateException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Create(CreditCard cc)
        {
            await _Repo.AddCreditCard(cc);
            await _Repo.Save();

            return CreatedAtRoute("Get CreditCard", new { id = cc.Id }, cc);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            CreditCard cc = await _Repo.GetCreditCardById(id);
            if (cc == null)
            {
                return NotFound();
            }

            await _Repo.DeleteCreditCard(id);
            await _Repo.Save();

            return NoContent();
        }
    }
}
