using MovieDatabaseAPI.DTOs;
using MovieDatabaseAPI.Models;

namespace MovieDatabaseAPI.Mappers;

public static class MovieMapper
{
    public static MovieDto ToMovieDto(this Movie movie)
    {
        return new MovieDto
        {
            Id = movie.Id,
            Title = movie.Title,
            ReleaseYear = movie.ReleaseYear,
            Description = movie.Description,
            Reviews = movie.Reviews.Select(r => r.ToReviewForMovieDto()).ToList()
        };
    }
    
    public static MovieListDto ToMovieListDto(this Movie movie)
    {
        return new MovieListDto
        {
            Id = movie.Id,
            Title = movie.Title,
            ReleaseYear = movie.ReleaseYear
        };
    }

    public static Movie ToMovie(this MovieCreateDto movieCreateDto)
    {
        return new Movie
        {
            Title = movieCreateDto.Title,
            ReleaseYear = movieCreateDto.ReleaseYear,
            Description = movieCreateDto.Description
        };
    }

    public static Movie ToMovie(this MovieUpdateDto movieUpdateDto)
    {
        return new Movie
        {
            Id = movieUpdateDto.Id,
            Title = movieUpdateDto.Title,
            ReleaseYear = movieUpdateDto.ReleaseYear,
            Description = movieUpdateDto.Description
        };
    }
}