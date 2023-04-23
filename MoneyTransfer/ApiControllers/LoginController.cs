using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
//using Domains;
using Bl;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using System;

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
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(configuration["Jwt: Issuer"],
                configuration["Jwt: Issuer"],
                null,
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);

        } 
        #endregion
    } 
}
