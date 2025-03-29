using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MovieDatabaseAPI.Models;

[Index(nameof(Name), IsUnique = true)]
public class Genre
{
    public int Id { get; set; }

    [MaxLength(100)]
    public string Name { get; set; } = "";

    public ICollection<Movie> Movies { get; set; } = new List<Movie>();
}