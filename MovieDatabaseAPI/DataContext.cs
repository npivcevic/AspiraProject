using Microsoft.EntityFrameworkCore;
using MovieDatabaseAPI.Models;

namespace MovieDatabaseAPI;

public class DataContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Movie> Movies { get; set; }

    public DataContext(DbContextOptions options) : base(options)
    {
        
    }
}