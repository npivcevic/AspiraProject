using System.ComponentModel.DataAnnotations;
using MovieDatabaseAPI.Models;

namespace MovieDatabaseAPI.DTOs;

public class MovieDto
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public int? ReleaseYear { get; set; }
    public string Description { get; set; } = "";

    public List<ReviewForMovieDto> Reviews { get; set; } = new List<ReviewForMovieDto>();
    public List<GenreDto> Genres { get; set; } = new List<GenreDto>();

    public double AverageRating { get; set; } = 0;
}

public class MovieListDto
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public int? ReleaseYear { get; set; }
}

public class MovieCreateDto
{
    [StringLength(200)]
    public string Title { get; set; } = "";
    [Range(1800, 2100)]
    public int? ReleaseYear { get; set; }
    [StringLength(1000)]
    public string Description { get; set; } = "";
    public List<int> GenreIds { get; set; } = new List<int>();
}

public class MovieUpdateDto
{
    public int Id { get; set; }
    [StringLength(200)]
    public string Title { get; set; } = "";
    [Range(1800, 2100)]
    public int? ReleaseYear { get; set; }
    [StringLength(1000)]
    public string Description { get; set; } = "";
    public List<int> GenreIds { get; set; } = new List<int>();
}