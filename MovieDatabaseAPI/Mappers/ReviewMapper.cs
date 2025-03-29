using MovieDatabaseAPI.DTOs;
using MovieDatabaseAPI.Models;

namespace MovieDatabaseAPI.Mappers;

public static class ReviewMapper
{
    public static ReviewDto ToReviewDto(this Review review)
    {
        return new ReviewDto
        {
            Id = review.Id,
            Content = review.Content,
            Rating = review.Rating,
            CreatedAt = review.CreatedAt,
            UpdatedAt = review.UpdatedAt,
            User = review.User.ToUserListDto(),
            Movie = review.Movie.ToMovieListDto()
        };
    }

    public static ReviewListDto ToReviewListDto(this Review review)
    {
        return new ReviewListDto
        {
            Id = review.Id,
            Rating = review.Rating,
            CreatedAt = review.CreatedAt,
            User = review.User?.ToUserListDto(),
            Movie = review.Movie?.ToMovieListDto()
        };
    }

    public static ReviewForMovieDto ToReviewForMovieDto(this Review review)
    {
        return new ReviewForMovieDto
        {
            Id = review.Id,
            Rating = review.Rating,
            Content = review.Content,
            CreatedAt = review.CreatedAt,
            User = review.User?.ToUserListDto(),
        };
    }

    public static Review ToReview(this ReviewCreateDto reviewCreateDto)
    {
        return new Review
        {
            Content = reviewCreateDto.Content,
            Rating = reviewCreateDto.Rating,
            UserId = reviewCreateDto.UserId,
            MovieId = reviewCreateDto.MovieId,
            CreatedAt = DateTime.Now,
            UpdatedAt = null
        };
    }

    public static Review ToReview(this ReviewUpdateDto reviewUpdateDto, Review existingReview)
    {
        return new Review
        {
            Id = reviewUpdateDto.Id,
            Content = reviewUpdateDto.Content,
            Rating = reviewUpdateDto.Rating,
            UserId = existingReview.UserId,
            MovieId = existingReview.MovieId,
            CreatedAt = existingReview.CreatedAt,
            UpdatedAt = DateTime.Now
        };
    }
}