using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
//using Domains;
using Bl;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using System;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MoneyTransfer.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        // all  dependency injection definition :
        #region dependency injection region

        //Jwt:
        IConfiguration configuration;

        IBusinessLayer<TbUser> oClsUsers;
        public LoginController(IConfiguration iconfig, IBusinessLayer<TbUser> Users)
        {
            oClsUsers = Users;
            configuration= iconfig;
        }
        #endregion

        #region Login function
        // POST https://localhost:7255/api/login
        [HttpPost]
        // [Route("Login")] dont write this !
        [AllowAnonymous]
        public IActionResult Login([FromBody] TbUser user)
        {
            var response = Unauthorized();
           var xuser = oClsUsers.AuthorizeUser(user);
            if (xuser != null)
            {
                var newToken = GenerateToken(user);
                return Ok(new { token = newToken });//new obj has key named token with value newToken 
            }
            return response;
        }
        #endregion

        #region GenerateToken Function :
        string GenerateToken(TbUser user)
        {
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.Username),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.Role, "User")
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: credentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return token;
        }
        #endregion
    } 
}
