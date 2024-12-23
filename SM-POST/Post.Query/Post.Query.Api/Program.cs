using Microsoft.EntityFrameworkCore;
using Post.Query.Domain.Repositories;
using Post.Query.Infrastruture.DataAccess;
using Post.Query.Infrastruture.Handlers;
using Post.Query.Infrastruture.Repositories;


var builder = WebApplication.CreateBuilder(args);  // Creates a builder to configure the web application, passing command-line arguments

// Defines the DbContext configuration, using Lazy Loading and setting up the connection to SQL Server
Action<DbContextOptionsBuilder> configureDbContext = o => o.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));

// Registers the DatabaseContext in the dependency injection (DI) container with the DbContext configuration
builder.Services.AddDbContext<DatabaseContext>(configureDbContext);

// Registers the DatabaseContextFactory as a singleton, allowing the creation of DatabaseContext instances based on the configuration
builder.Services.AddSingleton<DatabaseContextFactory>(new DatabaseContextFactory(configureDbContext));

// Retrieves an instance of DatabaseContext from the DI container, allowing direct database access
var dataContext = builder.Services.BuildServiceProvider().GetRequiredService<DatabaseContext>();
dataContext.Database.EnsureCreated();


builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IEventHandler, Post.Query.Infrastruture.Handlers.EventHandler>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();  


if (app.Environment.IsDevelopment())  
{
    app.UseSwagger();  
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); 
app.UseAuthorization(); 
app.MapControllers();  
app.Run(); 
