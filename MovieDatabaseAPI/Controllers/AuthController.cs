using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieDatabaseAPI.DTOs;
using MovieDatabaseAPI.Mappers;
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
        public async Task<ActionResult<TokenResponse>> Login(LoginDto loginDto)
        {
            User? user = await _context.Users.Include(u => u.RefreshTokens)
                .FirstOrDefaultAsync(u => u.Email == loginDto.Email);
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

            var newRefreshToken = await CreateNewAndDeleteExpiredRefreshTokens(user);

            return Ok(new TokenResponse()
            {
                AccessToken = _tokenService.IssueToken(user),
                RefreshToken = newRefreshToken.Value
            });
        }

        [HttpPost("RefreshToken")]
        public async Task<ActionResult<TokenResponse>> RefreshToken(RefreshTokenRequest refreshTokenRequest)
        {
            User? user = await _context
                .Users
                .Include(u => u.RefreshTokens)
                .FirstOrDefaultAsync(u =>
                    u.RefreshTokens.Any(rt => rt.Value == refreshTokenRequest.RefreshToken));

            if (user == null)
            {
                return Unauthorized("Invalid refresh token");
            }

            var refreshToken = user.RefreshTokens.FirstOrDefault(rt => rt.Value == refreshTokenRequest.RefreshToken);
            if (refreshToken == null || refreshToken.ExpiresAt < DateTime.Now)
            {
                return Unauthorized("Refresh token expired");
            }

            user.RefreshTokens.Remove(refreshToken);
            var newRefreshToken = await CreateNewAndDeleteExpiredRefreshTokens(user);

            return Ok(new TokenResponse()
            {
                AccessToken = _tokenService.IssueToken(user),
                RefreshToken = newRefreshToken.Value
            });
        }


        [HttpPost("Logout")]
        public async Task<ActionResult<TokenResponse>> Logout(RefreshTokenRequest refreshTokenRequest)
        {
            User? user = await _context
                .Users
                .Include(u => u.RefreshTokens)
                .FirstOrDefaultAsync(u =>
                    u.RefreshTokens.Any(rt => rt.Value == refreshTokenRequest.RefreshToken));

            if (user == null)
            {
                return Unauthorized("Invalid refresh token");
            }

            var refreshToken = user.RefreshTokens.FirstOrDefault(rt => rt.Value == refreshTokenRequest.RefreshToken);
            if (refreshToken == null || refreshToken.ExpiresAt < DateTime.Now)
            {
                return Unauthorized("Refresh token expired");
            }

            user.RefreshTokens.Remove(refreshToken);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("LogoutFromAllDevices")]
        public async Task<ActionResult<TokenResponse>> LogoutFromAllDevices(RefreshTokenRequest refreshTokenRequest)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized("User Id not found in JWT token.");
            }

            int authenticatedUserId = int.Parse(userId);
            User? user = _context.Users.Include(u => u.RefreshTokens).FirstOrDefault(u => u.Id == authenticatedUserId);

            if (user == null)
            {
                return Unauthorized("User not found.");
            }

            user.RefreshTokens.Clear();
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("Me")]
        [Authorize]
        public async Task<ActionResult<UserDto>> Me()
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized("User Id not found in JWT token.");
            }

            int authenticatedUserId = int.Parse(userId);
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.Id == authenticatedUserId);
            if (user == null)
            {
                return Unauthorized("User not found.");
            }

            return Ok(user.ToUserDto());
        }

        private async Task<RefreshToken> CreateNewAndDeleteExpiredRefreshTokens(User user)
        {
            var expiredRefreshTokens = user.RefreshTokens.Where(rt => rt.ExpiresAt > DateTime.Now).ToList();
            foreach (var expiredRefreshToken in expiredRefreshTokens)
            {
                user.RefreshTokens.Remove(expiredRefreshToken);
            }

            RefreshToken newRefreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshTokens.Add(newRefreshToken);

            await _context.SaveChangesAsync();
            return newRefreshToken;
        }
    }
}