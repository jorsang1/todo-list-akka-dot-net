using Akka.Actor;
using Akka.TestKit.Xunit2;
using TodoListAkka.Application;

public class TaskListActorTests : TestKit
{
    [Fact]
    public async Task TaskListActor_Should_Add_And_Remove_Tasks()
    {
        // Arrange
        var actorRef = ActorOfAsTestActorRef<TaskListActor>(Props.Create(() => new TaskListActor()));
        var taskId = "Task1";
        var description = "Test Task";

        // Act
        await actorRef.Ask<string>(new AddTask(taskId, description));
        var tasksBeforeRemove = await actorRef.Ask<TaskListResponse>(new GetTasks());
        await actorRef.Ask<string>(new RemoveTask(taskId));
        var tasksAfterRemove = await actorRef.Ask<TaskListResponse>(new GetTasks());

        // Assert
        Assert.Single(tasksBeforeRemove.Tasks);
        Assert.Equal(description, tasksBeforeRemove.Tasks.First());
        Assert.Empty(tasksAfterRemove.Tasks);
    }

    [Fact]
    public async Task TaskListActor_Should_Handle_Multiple_Tasks()
    {
        // Arrange
        var actorRef = ActorOfAsTestActorRef<TaskListActor>(Props.Create(() => new TaskListActor()));
        var task1 = new AddTask("Task1", "Task 1 Description");
        var task2 = new AddTask("Task2", "Task 2 Description");

        // Act
        await actorRef.Ask<string>(task1);
        await actorRef.Ask<string>(task2);
        var tasks = await actorRef.Ask<TaskListResponse>(new GetTasks());

        // Assert
        Assert.Equal(2, tasks.Tasks.Count());
        Assert.Contains(tasks.Tasks, t => t == "Task 1 Description");
        Assert.Contains(tasks.Tasks, t => t == "Task 2 Description");
    }
}