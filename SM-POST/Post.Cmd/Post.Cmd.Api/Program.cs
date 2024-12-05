using Confluent.Kafka;
using CQRS.Core.Domain;
using CQRS.Core.Handlers;
using CQRS.Core.Infrastructure;
using CQRS.Core.Producers;
using Post.Cmd.Api.Commands;
using Post.Cmd.Domain.Aggregates;
using Post.Cmd.Infrastruture.Config;
using Post.Cmd.Infrastruture.Dispatchers;
using Post.Cmd.Infrastruture.Handlers;
using Post.Cmd.Infrastruture.Producers;
using Post.Cmd.Infrastruture.Repositories;
using Post.Cmd.Infrastruture.Stores;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//env variables injection
builder.Services.Configure<MongoDbConfig>(builder.Configuration.GetSection(nameof(MongoDbConfig)));
builder.Services.Configure<ProducerConfig>(builder.Configuration.GetSection(nameof(ProducerConfig)));

// dependency injection
builder.Services.AddScoped<IEventStoreRepository, EventStoreRepository>();
builder.Services.AddScoped<IEventStore,EventStore>();
builder.Services.AddScoped<IEventSourcingHandler<PostAggregate>, EventSourcingHandler>();
builder.Services.AddScoped<IEventProducer, EventProducer>();
builder.Services.AddScoped<ICommandHandler, CommandHandler>();

// dependency injection to commands
var dispatcher = new CommandDispatcher();
var commandHandler = builder.Services.BuildServiceProvider().GetRequiredService<ICommandHandler>();
dispatcher.RegisterHandler<NewPostCommand>(commandHandler.HandlerAsync);
dispatcher.RegisterHandler<EditMessageCommand>(commandHandler.HandlerAsync);
dispatcher.RegisterHandler<LikePostCommand>(commandHandler.HandlerAsync);
dispatcher.RegisterHandler<AddPostCommentCommand>(commandHandler.HandlerAsync);
dispatcher.RegisterHandler<EditPostCommentCommand>(commandHandler.HandlerAsync);
dispatcher.RegisterHandler<RemovePostCommentCommand>(commandHandler.HandlerAsync);
dispatcher.RegisterHandler<DeletePostCommand>(commandHandler.HandlerAsync);
builder.Services.AddSingleton<ICommandDispatcher>(_ => dispatcher);

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
