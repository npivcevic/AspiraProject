using System.ComponentModel.DataAnnotations;
using MovieDatabaseAPI.Models;

namespace MovieDatabaseAPI.DTOs;

public class ReviewDto
{
    public int Id { get; set; }
    public string Content { get; set; } = "";
    public int Rating { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public required UserListDto User { get; set; }
    public required MovieListDto Movie { get; set; }
}

public class ReviewListDto
{
    public int Id { get; set; }
    public int Rating { get; set; }
    public DateTime CreatedAt { get; set; }

    public UserListDto? User { get; set; }
    public MovieListDto? Movie { get; set; }
}

public class ReviewForMovieDto
{
    public int Id { get; set; }
    public int Rating { get; set; }

    public string Content { get; set; } = "";
    public DateTime CreatedAt { get; set; }

    public UserListDto? User { get; set; }
}

public class ReviewCreateDto
{
    [StringLength(1000)]
    public string Content { get; set; } = "";
    [Range(1, 10)]
    public int Rating { get; set; }
    public int UserId { get; set; }
    public int MovieId { get; set; }
}

public class ReviewUpdateDto
{
    public int Id { get; set; }
    [StringLength(1000)]
    public string Content { get; set; } = "";
    [Range(1, 10)]
    public int Rating { get; set; }
}