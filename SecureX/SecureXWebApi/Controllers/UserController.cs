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
    [ApiController]
    public class UserController : Controller
    {
        private readonly ISecureXRepository IRepo;

        public UserController(ISecureXRepository Repo)
        {
            IRepo = Repo;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<IEnumerable<User>> GetAll()
        {
            var userlist = await IRepo.GetUsers();
            return userlist;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [FormatFilter]
        public async Task<ActionResult<User>> GetById(int id)
        {
            if(!ModelState.IsValid)
            {
                return NotFound();
            }
            try
            {
                var user = await IRepo.GetUserById(id);
                return user;
            }
            catch(DbUpdateException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]User user)
        {
            user.Id = 0;
            await IRepo.AddUser(user);
            await IRepo.Save();

            return NoContent();
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] User user)
        {
            User selectUser = await IRepo.GetUserById(user.Id);

            if(selectUser == null)
            {
                return NotFound();
            }

            selectUser.FirstName = user.FirstName;
            selectUser.LastName = user.LastName;
            await IRepo.Save();

            return NoContent();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            User selectUser = await IRepo.GetUserById(id);
            if(selectUser == null)
            {
                return NotFound();
            }

            await IRepo.DeleteUser(id);
            await IRepo.Save();

            return NoContent();
        }
    }
}
