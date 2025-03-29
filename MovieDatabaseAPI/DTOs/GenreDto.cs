using System.ComponentModel.DataAnnotations;

namespace MovieDatabaseAPI.DTOs;

public class GenreDto
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
}

public class GenreListDto
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
}


public class GenreCreateDto
{
    [StringLength(100)]
    public string Name { get; set; } = "";
}

public class GenreUpdateDto
{
    public int Id { get; set; }
    [StringLength(100)]
    public string Name { get; set; } = "";
}
