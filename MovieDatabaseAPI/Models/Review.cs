using System.ComponentModel.DataAnnotations;

namespace MovieDatabaseAPI.Models;

public class Review
{
    public int Id { get; set; }
    [MaxLength(1000)]
    public string Content { get; set; } = "";
    public int Rating { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public int UserId { get; set; }
    public User? User { get; set; }

    public int MovieId { get; set; }
    public Movie? Movie { get; set; }
}