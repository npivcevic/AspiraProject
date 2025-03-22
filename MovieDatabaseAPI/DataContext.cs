using Microsoft.EntityFrameworkCore;

namespace MovieDatabaseAPI;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
        
    }
}