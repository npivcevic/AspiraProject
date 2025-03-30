using System.ComponentModel.DataAnnotations;
using MovieDatabaseAPI.CustomValidators;

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
    [UniqueEmail]
    [EmailAddress]
    [StringLength(100)]
    public required string Email { get; set; }
    [StringLength(100)]
    public required string Password { get; set; }
    [StringLength(100)]
    public string FirstName { get; set; } = "";
    [StringLength(100)]
    public string LastName { get; set; } = "";
}

public class UserUpdateDto
{
    public int Id { get; set; }
    [UniqueEmail]
    [EmailAddress]
    [StringLength(100)]
    public required string Email { get; set; }
    [StringLength(100)]
    public required string Password { get; set; }
    [StringLength(100)]
    public string FirstName { get; set; } = "";
    [StringLength(100)]
    public string LastName { get; set; } = "";
}