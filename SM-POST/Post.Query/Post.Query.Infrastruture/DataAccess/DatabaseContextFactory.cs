using Microsoft.EntityFrameworkCore;

namespace Post.Query.Infrastruture.DataAccess;

public class DatabaseContextFactory
{
    private readonly Action<DbContextOptionsBuilder> _configureDbContext;

    public DatabaseContextFactory(Action<DbContextOptionsBuilder> configureDbContext)
    {
       _configureDbContext = configureDbContext; 
    }

    public DatabaseContext CreateDbContext()
    {
        DbContextOptionsBuilder<DatabaseContext> options = new();
        _configureDbContext(options);
        
        return new DatabaseContext(options.Options);
    }
}