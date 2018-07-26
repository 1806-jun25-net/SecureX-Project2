﻿using System;
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
    public class UserController : Controller
    {
        private readonly SecureXRepository _Repo;

        public UserController(SecureXRepository Repo)
        {
            _Repo = Repo;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var userlist = await _Repo.GetUsers();
            return Ok(userlist);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(int x)
        {
            if(!ModelState.IsValid)
            {
                return NotFound();
            }
            try
            {
                var user = await _Repo.GetUserById(x);
                return Ok(user);
            }
            catch(DbUpdateException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Create(User user)
        {
            _Repo.AddUser(user);
            _Repo.Save();

            return CreatedAtRoute("Get User", new { id = user.Id }, user);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(String password, User user)
        {
            User selectUser = await _Repo.GetUserById(user.Id);

            if(selectUser == null)
            {
                return NotFound();
            }

            selectUser.Password = password;
            selectUser.Password = user.Password;
            _Repo.Save();

            return NoContent();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            User selectUser = await _Repo.GetUserById(id);
            if(selectUser == null)
            {
                return NotFound();
            }

            _Repo.DeleteUser(id);
            _Repo.Save();

            return NoContent();
        }
    }
}
