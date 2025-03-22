using System.ComponentModel.DataAnnotations;

namespace MovieDatabaseAPI.Models;

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
}