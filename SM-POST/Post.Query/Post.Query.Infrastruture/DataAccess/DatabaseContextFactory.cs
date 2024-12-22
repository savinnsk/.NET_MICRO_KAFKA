// Importing the necessary namespace from Entity Framework Core
using Microsoft.EntityFrameworkCore;

namespace Post.Query.Infrastruture.DataAccess
{
    // This class is a factory used to create instances of the DatabaseContext.
    // It allows the configuration of the DbContext options in a flexible manner,
    // separating the setup logic from the actual DbContext usage.

    public class DatabaseContextFactory
    {
        // A private field that holds the action to configure the DbContext options.
        // The Action<DbContextOptionsBuilder> allows the user to define how the DbContext should be configured (e.g., connection string, logging).
        private readonly Action<DbContextOptionsBuilder> _configureDbContext;

        // Constructor that takes an action to configure the DbContextOptionsBuilder.
        // The action will be applied to configure the DbContext when it is created.
        public DatabaseContextFactory(Action<DbContextOptionsBuilder> configureDbContext)
        {
            // Storing the provided action for later use
            _configureDbContext = configureDbContext;
        }

        // This method creates a new instance of the DatabaseContext.
        // It applies the configuration action to a new DbContextOptionsBuilder and returns a new DatabaseContext.
        public DatabaseContext CreateDbContext()
        {
            // Create a new DbContextOptionsBuilder for the DatabaseContext.
            DbContextOptionsBuilder<DatabaseContext> options = new();

            // Apply the configuration action to the options builder to configure DbContext.
            _configureDbContext(options);

            // Return a new instance of DatabaseContext using the configured options.
            return new DatabaseContext(options.Options);
        }
    }
}
