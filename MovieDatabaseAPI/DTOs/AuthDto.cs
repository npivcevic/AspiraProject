using System.ComponentModel.DataAnnotations;

namespace MovieDatabaseAPI.DTOs;

public class LoginDto
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    public required string Password { get; set; }
}

public class TokenResponse
{
    public string AccessToken { get; set; } = "";

    public string RefreshToken { get; set; } = "";
}

public class RefreshTokenRequest
{
    [Required]
    public required string RefreshToken { get; set; }
}