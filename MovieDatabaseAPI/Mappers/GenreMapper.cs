using MovieDatabaseAPI.DTOs;
using MovieDatabaseAPI.Models;

namespace MovieDatabaseAPI.Mappers;

public static class GenreMapper
{
    public static GenreDto ToGenreDto(this Genre genre)
    {
        return new GenreDto
        {
            Id = genre.Id,
            Name = genre.Name
        };
    }
    
    public static GenreListDto ToGenreListDto(this Genre genre)
    {
        return new GenreListDto
        {
            Id = genre.Id,
            Name = genre.Name
        };
    }

    public static Genre ToGenre(this GenreCreateDto genreCreateDto)
    {
        return new Genre
        {
            Name = genreCreateDto.Name
        };
    }

    public static Genre ToGenre(this GenreUpdateDto genreUpdateDto)
    {
        return new Genre()
        {
            Id = genreUpdateDto.Id,
            Name = genreUpdateDto.Name
        };
    }
}