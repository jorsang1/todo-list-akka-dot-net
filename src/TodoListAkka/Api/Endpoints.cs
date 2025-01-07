using Akka.Actor;
using Microsoft.AspNetCore.Builder;
using TodoListAkka.Application;

namespace TodoListAkka.Api;

public static class Endpoints
{
    public static void MapTaskEndpoints(this WebApplication app, ActorSystem actorSystem, IActorRef taskListActor)
    {
        // Add a new task
        app.MapPost("/tasks", async (AddTaskRequest request) =>
        {
            var result = await taskListActor.Ask<string>(new AddTask(request.TaskId, request.Description));
            return Results.Ok(result);
        });

        // Get all tasks
        app.MapGet("/tasks", async () =>
        {
            var result = await taskListActor.Ask<TaskListResponse>(new GetTasks());
            return Results.Ok(result.Tasks);
        });

        // Get a specific task by ID
        app.MapGet("/tasks/{taskId}", async (string taskId) =>
        {
            var actorPath = taskListActor.Path / $"Task-{taskId}";
            var taskActor = actorSystem.ActorSelection(actorPath);
            
            if (Equals(taskActor, ActorRefs.Nobody))
                return Results.NotFound($"Task {taskId} not found.");

            var result = await taskActor.Ask<TaskState>(new GetTaskState());
            return Results.Ok(result);
        });

        // Update a task
        app.MapPut("/tasks/{taskId}", async (string taskId, UpdateTaskRequest request) =>
        {
            var result = await taskListActor.Ask<string>(new AddTask(taskId, request.Description));
            return Results.Ok(result);
        });

        // Delete a task
        app.MapDelete("/tasks/{taskId}", async (string taskId) =>
        {
            var result = await taskListActor.Ask<string>(new RemoveTask(taskId));
            return Results.Ok(result);
        });
    }
}