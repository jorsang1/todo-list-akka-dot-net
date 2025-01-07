using Akka.Actor;

namespace TodoListAkka.Application;

public class TaskActor : ReceiveActor
{
    private readonly string _taskId;
    private string _description;

    public TaskActor(string taskId)
    {
        _taskId = taskId;
        _description = string.Empty;

        ConfigureMessageHandlers();
    }

    private void ConfigureMessageHandlers()
    {
        Receive<UpdateTask>(HandleUpdateTask);
        Receive<GetTaskState>(HandleGetTaskState);
    }

    private void HandleUpdateTask(UpdateTask message)
    {
        _description = message.Description;
        Sender.Tell($"Task {_taskId} updated.");
    }

    private void HandleGetTaskState(GetTaskState message)
    {
        Sender.Tell(new TaskState(_taskId, _description));
    }
}