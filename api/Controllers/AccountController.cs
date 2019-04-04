using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Fisher.Bookstore.Api.Data;
using Fisher.Bookstore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Fisher.Bookstore.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public AccountController(UserManager<ApplicationUser> userManager,
                SignInManager<ApplicationUser> signInManager,
                IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }

        public UserManager<ApplicationUser> userManager { get; set; }
        public SignInManager<ApplicationUser> signInManager { get; set; }
        public IConfiguration configuration { get; set; }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] ApplicationUser login)
        {
            var result = await signInManager.PasswordSignInAsync(login.Email,
            login.Password, isPersistent: false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                return Unauthorized();
            }
        ApplicationUser user = await userManager.FindByEmailAsync(login.Email);
        JwtSecurityToken token = GenerateToken(user);
        string serializedToken = new JwtSecurityTokenHandler().WriteToken(token);
        return Ok();
        }

        private JwtSecurityToken GenerateToken(ApplicationUser user)
        {
            var claims = new List<Claim>();

            var expirationDays = configuration.GetValue<int>
            ("JWTConfiguration:TokenExpirationDays");

            var signingKey = Encoding.UTF8.GetBytes(configuration.GetValue<string>
            ("JWTConfiguration:Key"));

            var token = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("JWTConfiguration:Issuer"),
                audience: configuration.GetValue<string>("JWTConfiguration:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromDays(expirationDays)),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(new
            SymmetricSecurityKey(signingKey), SecurityAlgorithms.HmacSha256));

            return token;
        }
    }
}