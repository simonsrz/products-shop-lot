using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Shop_API.Data;
using Shop_API.Dto;
using Shop_API.Models;
using Shop_API.Response;
using Shop_API.Services;

namespace Shop_API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly ShopContext _shopDbContext;
        private readonly TokenService _tokenService;

        public AuthenticationController(ShopContext shopDbContext, TokenService tokenService)
        {
            _shopDbContext = shopDbContext;
            _tokenService = tokenService; 
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UserDto> Register(UserDto request)
        {
            User newUser = new User(0, request.username, request.password);
            _shopDbContext.Users.Add(newUser);
            _shopDbContext.SaveChanges();

            return CreatedAtAction(nameof(Register), new { username = newUser.username }, newUser);
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<LoginResponse> Authenticate([FromBody] UserDto request)
        {

            var user = _shopDbContext.Users.FirstOrDefault(u => u.username == request.username && u.password == request.password);
            if (user == null)
            {
                return BadRequest("Wrong credentials");
            }

            var jwtToken = _tokenService.CreateToken(user);
            _shopDbContext.SaveChanges();
            return Ok(new LoginResponse
            {
                Token = jwtToken
            });
        }
    }
}
