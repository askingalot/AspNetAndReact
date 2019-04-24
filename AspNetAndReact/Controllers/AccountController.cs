using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AspNetAndReact.Data;
using AspNetAndReact.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AspNetAndReact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register(Registration registration)
        {
            try
            {
                var userNameExists =
                    await _context.Users.AnyAsync(u =>
                        u.UserName.ToUpper() == registration.Email.ToUpper());

                if (userNameExists)
                {
                    return Conflict("User Name already taken");
                }

                var newUser = new ApplicationUser
                {
                    FirstName = registration.FirstName,
                    LastName = registration.LastName,
                    UserName = registration.Email,
                    Email = registration.Email
                };

                var result = await _userManager.CreateAsync(newUser, registration.Password);
                if (result.Succeeded)
                {
                    return new ObjectResult(GenerateToken(newUser.UserName));
                }

                return BadRequest();
            }
            catch
            {
                throw new Exception("Unknown Error");
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(Login login)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(
                    login.Email, login.Password, isPersistent: false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return new ObjectResult(GenerateToken(login.Email));
                }

                return BadRequest("Bad username or password");
            }
            catch
            {
                throw new Exception("Unknown Error");
            }
        }

        [Authorize]
        [HttpGet("[action]")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return Ok();
            }
            catch
            {
                throw new Exception("Unknown Error");
            }
        }


        private string GenerateToken(string username)
        {
            var claims = new Claim[] {
                new Claim(ClaimTypes.Name, username),
                new Claim(JwtRegisteredClaimNames.Nbf, 
                    new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, 
                    new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString ()),
            };

            var token = new JwtSecurityToken(
                new JwtHeader(new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes("7A735D7B-1A19-4D8A-9CFA-99F55483013F")),
                    SecurityAlgorithms.HmacSha256)),
                new JwtPayload(claims)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}