using CQRS.Core.Domain;
using Post.Cmd.Infrastruture.Config;
using Post.Cmd.Infrastruture.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.Configure<MongoDbConfig>(builder.Configuration.GetSection(nameof(MongoDbConfig)));
builder.Services.AddScoped<IEventStoreRepository, EventStoreRepository>();

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
