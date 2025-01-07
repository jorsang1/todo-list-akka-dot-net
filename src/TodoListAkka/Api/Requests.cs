namespace TodoListAkka.Api;

// DTOs for HTTP requests
public record AddTaskRequest(string TaskId, string Description);
public record UpdateTaskRequest(string Description);