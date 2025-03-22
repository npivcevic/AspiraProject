using System.ComponentModel.DataAnnotations;

namespace MovieDatabaseAPI.DTOs;

public class UserDto
{
    public int Id { get; set; }
    public required string Email { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
}

public class UserListDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
}

public class UserCreateDto
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
}

public class UserUpdateDto
{
    public int Id { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
}