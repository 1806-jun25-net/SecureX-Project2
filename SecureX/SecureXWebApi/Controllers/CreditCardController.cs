﻿using System;
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
    [ApiController]
    public class CreditCardController : Controller
    {
        private readonly ISecureXRepository IRepo;

        public CreditCardController(ISecureXRepository Repo)
        {
            IRepo = Repo;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<IEnumerable<CreditCard>> GetAll()
        {
            var cclist = await IRepo.GetCreditCards();
            return cclist;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [FormatFilter]
        public async Task<ActionResult<CreditCard>> GetById(int id)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            try
            {
                var creditc = await IRepo.GetCreditCardById(id);
                return creditc;
            }
            catch (DbUpdateException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(CreditCard creditCard)
        {
            CreditCard selectCC = await IRepo.GetCreditCardById(creditCard.Id);


            if (selectCC == null)
            {
                return NotFound();
            }

            selectCC.Status = creditCard.Status;
            selectCC.CreditLine = creditCard.CreditLine;
            selectCC.CreditLimit = creditCard.CreditLimit;
            selectCC.CurrentDebt = creditCard.CurrentDebt;
            await IRepo.UpdateCreditCard(selectCC);
            await IRepo.Save();

            return NoContent();
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Create(CreditCard cc)
        {
            cc.Id = 0;
            await IRepo.AddCreditCard(cc);
            await IRepo.Save();

            return NoContent();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            CreditCard cc = await IRepo.GetCreditCardById(id);
            if (cc == null)
            {
                return NotFound();
            }

            await IRepo.DeleteCreditCard(id);
            await IRepo.Save();

            return NoContent();
        }
    }
}
