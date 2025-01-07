using Akka.Actor;
using TodoListAkka.Api;
using TodoListAkka.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Set up the actor system
var actorSystem = ActorSystem.Create("TodoListSystem");
var taskListActor = actorSystem.ActorOf(Props.Create(() => new TaskListActor()), "TaskListActor");

// Map endpoints
app.MapTaskEndpoints(actorSystem, taskListActor);

app.UseHttpsRedirection();

// Run the application
app.Run();