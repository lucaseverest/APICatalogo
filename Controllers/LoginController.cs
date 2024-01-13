using APICatalogo.Models;
using APICatalogo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace APICatalogo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _configuration;
        public LoginController(IConfiguration iConfig)
        {
            _configuration = iConfig;
        }

        [HttpPost, AllowAnonymous]
        public async Task<ActionResult> Login(UserModel userModel, ITokenService tokenService)
        {
            if (userModel == null)
            {
                return BadRequest("Login Inválido");
            }
            if (userModel.UserName == "lucas" && userModel.Password == "123456")
            {
                var tokenString = tokenService.GerarToken(_configuration["Jwt:Key"],
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    userModel);
                return Ok(new { token = tokenString });
            }
            else
            {
                return BadRequest("Login Inválido");
            }
        }
    }
}
