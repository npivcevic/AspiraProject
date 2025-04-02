using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieDatabaseAPI.DTOs;
using MovieDatabaseAPI.Models;
using MovieDatabaseAPI.Services;

namespace MovieDatabaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly TokenService _tokenService;

        public AuthController(DataContext context, IPasswordHasher<User> passwordHasher, TokenService tokenService)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
        }

        [HttpPost("Login")]
        public ActionResult<TokenResponse> Login(LoginDto loginDto)
        {
            User? user = _context.Users.FirstOrDefault(u => u.Email == loginDto.Email);
            if (user == null)
            {
                return Unauthorized();
            }

            PasswordVerificationResult result =
                _passwordHasher.VerifyHashedPassword(user, user.Password, loginDto.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                return Unauthorized();
            }

            return Ok(new TokenResponse() { AccessToken = _tokenService.IssueToken(user) });
        }
    }
}