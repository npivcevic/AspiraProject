using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MovieDatabaseAPI.Models;

[Index(nameof(Email), IsUnique = true)]
public class User
{
    public int Id { get; set; }
    [MaxLength(100)]
    public required string Email { get; set; }
    [MaxLength(100)]
    public required string Password { get; set; }
    [MaxLength(100)]
    public string FirstName { get; set; } = "";
    [MaxLength(100)]
    public string LastName { get; set; } = "";

    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
}