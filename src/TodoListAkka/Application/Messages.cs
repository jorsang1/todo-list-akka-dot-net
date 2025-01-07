namespace TodoListAkka.Application;

public record AddTask(string TaskId, string Description);
public record RemoveTask(string TaskId);
public record GetTasks();
public record TaskListResponse(IEnumerable<string> Tasks);

public record UpdateTask(string Description);
public record GetTaskState();
public record TaskState(string TaskId, string Description);