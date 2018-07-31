using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SecureXLibrary;
using SecureXWebApi.Models;

namespace SecureXWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private SignInManager<IdentityUser> _signInManager { get; }

        public LoginController(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Login(Login input)
        {
            var result = await _signInManager.PasswordSignInAsync(input.UserName, input.Password,
                isPersistent: false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                return StatusCode(400); // Bad Request
            }

            return NoContent();
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(204)]
        public async Task<NoContentResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return NoContent();
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Register(Login input,
            [FromServices] UserManager<IdentityUser> userManager,
            [FromServices] RoleManager<IdentityRole> roleManager, bool employee = false)
        {
            // with an [ApiController], model state is always automatically checked
            // and return 400 if any errors.

            var user = new IdentityUser(input.UserName);
            var result = await userManager.CreateAsync(user, input.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result);
            }

            if (employee)
            {
                if (!(await roleManager.RoleExistsAsync("employee")))
                {
                    var employeeRole = new IdentityRole("employee");
                    result = await roleManager.CreateAsync(employeeRole);
                    if (!result.Succeeded)
                    {
                        return StatusCode(400, result);
                    }
                }
                result = await userManager.AddToRoleAsync(user, "employee");
                if (!result.Succeeded)
                {
                    return StatusCode(400, result);
                }
            }

            await _signInManager.SignInAsync(user, isPersistent: false);

            return NoContent();
        }
    }
}
