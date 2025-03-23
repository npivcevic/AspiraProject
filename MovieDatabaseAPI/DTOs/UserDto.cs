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