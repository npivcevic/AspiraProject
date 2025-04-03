namespace MovieDatabaseAPI.Models;

public class RefreshToken
{
    public int Id { get; set; }

    public required string Value { get; set; }

    public DateTime ExpiresAt { get; set; }
    
    public int UserId { get; set; }

    public User? User { get; set; }
}