using Microsoft.EntityFrameworkCore;
using MovieDatabaseAPI.Models;

namespace MovieDatabaseAPI;

public class DataContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    public DataContext(DbContextOptions options) : base(options)
    {
        
    }
}