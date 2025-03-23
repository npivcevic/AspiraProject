using System.ComponentModel.DataAnnotations;

namespace MovieDatabaseAPI.Models;

public class Movie
{
    public int Id { get; set; }
    [MaxLength(200)]
    public string Title { get; set; } = "";
    public int? ReleaseYear { get; set; }
    [MaxLength(1000)]
    public string Description { get; set; } = "";
}