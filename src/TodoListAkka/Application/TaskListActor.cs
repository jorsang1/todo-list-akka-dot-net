using Akka.Actor;
using System.Collections.Generic;

namespace TodoListAkka.Application;

public class TaskListActor : ReceiveActor
{
    private readonly Dictionary<string, IActorRef> _tasks = new();

    public TaskListActor()
    {
        ConfigureMessageHandlers();
    }

    private void ConfigureMessageHandlers()
    {
        Receive<AddTask>(HandleAddTask);
        Receive<RemoveTask>(HandleRemoveTask);
        Receive<GetTasks>(HandleGetTasks);
    }

    private void HandleAddTask(AddTask message)
    {
        if (!_tasks.ContainsKey(message.TaskId))
        {
            var taskActor = Context.ActorOf(Props.Create(() => new TaskActor(message.TaskId)), $"Task-{message.TaskId}");
            _tasks[message.TaskId] = taskActor;
        }

        _tasks[message.TaskId].Tell(new UpdateTask(message.Description), Sender);
    }

    private void HandleRemoveTask(RemoveTask message)
    {
        if (_tasks.TryGetValue(message.TaskId, out var taskActor))
        {
            Context.Stop(taskActor);
            _tasks.Remove(message.TaskId);
            Sender.Tell($"Task {message.TaskId} removed.");
        }
        else
        {
            Sender.Tell($"Task {message.TaskId} not found.");
        }
    }

    private void HandleGetTasks(GetTasks message)
    {
        var taskStates = new List<TaskState>();
        foreach (var taskActor in _tasks.Values)
        {
            var state = taskActor.Ask<TaskState>(new GetTaskState()).Result;
            taskStates.Add(state);
        }
        var taskDescriptions = taskStates.Select(state => state.Description).ToList();
        Sender.Tell(new TaskListResponse(taskDescriptions));
    }
}