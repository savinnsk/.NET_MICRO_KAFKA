// Importing necessary namespaces from Entity Framework Core and the project domain
using Microsoft.EntityFrameworkCore;
using Post.Query.Domain;
using Post.Query.Domain.Entities;

namespace Post.Query.Infrastruture.DataAccess
{
    // This class represents the database context that will be used to interact with the database.
    // It inherits from DbContext, which is the main class used by Entity Framework Core to perform CRUD operations.
    public class DatabaseContext : DbContext
    {
        // DbSet properties represent tables in the database.
        // These properties will be used to query and save data for PostEntity and CommentEntity.
        
        // Represents the "Posts" table in the database.
        public DbSet<PostEntity> Posts { get; set; }
        
        // Represents the "Comments" table in the database.
        public DbSet<CommentEntity> Comments { get; set; }

        // The constructor receives the options to configure the context, such as the database connection string.
        // It passes these options to the base class (DbContext) constructor to properly initialize the context.
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }
    }
}
