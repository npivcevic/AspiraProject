using MovieDatabaseAPI.DTOs;
using MovieDatabaseAPI.Models;

namespace MovieDatabaseAPI.Mappers;

public static class UserMapper
{
    public static UserDto ToUserDto(this User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName
        };
    }
    
    public static UserListDto ToUserListDto(this User user)
    {
        return new UserListDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName
        };
    }
    
    public static User ToUser(this UserCreateDto userCreateDto)
    {
        return new User
        {
            Email = userCreateDto.Email,
            Password = userCreateDto.Password,
            FirstName = userCreateDto.FirstName,
            LastName = userCreateDto.LastName
        };
    }
    
    public static User ToUser(this UserUpdateDto userUpdateDto)
    {
        return new User
        {
            Id = userUpdateDto.Id,
            Email = userUpdateDto.Email,
            Password = userUpdateDto.Password,
            FirstName = userUpdateDto.FirstName,
            LastName = userUpdateDto.LastName
        };
    }
}