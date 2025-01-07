using Akka.Actor;
using Akka.TestKit.Xunit2;
using TodoListAkka.Application;

public class TaskActorTests : TestKit
{
    [Fact]
    public async Task TaskActor_Should_Update_And_Return_State()
    {
        // Arrange
        var taskId = "Task1";
        var description = "Test Task";
        var actorRef = ActorOfAsTestActorRef<TaskActor>(Props.Create(() => new TaskActor(taskId)));

        // Act
        await actorRef.Ask<string>(new UpdateTask(description));
        var state = await actorRef.Ask<TaskState>(new GetTaskState());

        // Assert
        Assert.Equal(taskId, state.TaskId);
        Assert.Equal(description, state.Description);
    }
}