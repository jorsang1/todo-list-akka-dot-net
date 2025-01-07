# TodoListAkka: Actor Model in .NET

## Introduction
This project demonstrates the **Actor Model** in .NET using [Akka.NET](https://getakka.net/).

It showcases how the actor model can simplify building concurrent, scalable, and fault-tolerant systems with a simple **To-Do List** application.

I've been always amazed by the Actor Model and how it can nowadays be enhanced by the cloud providing a new way to build distributed software.

Actors encapsulate state and behavior, communicate through asynchronous messages, and can be distributed across multiple nodes.  

The state in Akka is always hot in memory providing great response times, and it persists asynchronously to a journal using Even sourcing in a way that if the system crashes it can be restored to the last state. 

I'd also like to continue exploring other actor model implementations for .NET like [Orleans](https://learn.microsoft.com/en-us/dotnet/orleans/overview) and DAPR ([Actors in DAPR](https://docs.dapr.io/developing-applications/building-blocks/actors/actors-overview/)) in future repositories.

## Why Choose the Actor Model?

The Actor Model offers a robust and efficient way to handle concurrency and state management in distributed systems. Here are some key reasons to consider using the Actor Model:

1. **Isolation**: Each actor encapsulates its state and behavior, which prevents race conditions and makes the system more predictable. This isolation ensures that actors do not share state directly, reducing the complexity of managing concurrent operations.

2. **Concurrency**: Actors process messages asynchronously, allowing for high-performance and scalable applications. By decoupling message sending from message processing, the Actor Model enables efficient use of system resources and simplifies the development of concurrent applications.

3. **Fault Tolerance**: Parent actors can monitor and manage the lifecycle of child actors, enabling automatic recovery from failures. This supervision strategy allows the system to gracefully handle errors and continue operating without significant downtime.

4. **Scalability**: Actors can be distributed across multiple nodes, making it easy to scale horizontally as your application grows. The Actor Model's inherent support for distribution allows developers to build systems that can handle increasing loads by simply adding more nodes.

5. **Simplified Design**: The Actor Model abstracts away low-level threading and synchronization concerns, allowing developers to focus on business logic. This abstraction reduces the cognitive load on developers and leads to cleaner, more maintainable code.

6. **Domain-Driven Design (DDD)**: The Actor Model aligns well with DDD principles, making it easier to model complex business domains. Actors can represent domain entities, aggregates, and services, providing a natural way to implement DDD patterns.

7. **Event Sourcing**: With Akka persistence, event sourcing is supported out of the box, allowing you to store the state of your actors as a series of events. This approach provides a reliable audit trail and enables powerful features like time travel and state reconstruction.

8. **Single Responsibility Principle (SRP)**: Each actor encapsulates a specific business behavior, making it easy to align with SRP and maintain clean, modular code. By adhering to SRP, actors become easier to understand, test, and maintain.

9. **Business Behavior**: The abstractions of Ask, Tell, Process, and Store make it straightforward to match business behavior with actor interactions. These abstractions provide a clear and intuitive way to model business processes and workflows.

By leveraging these benefits, the Actor Model can help you build resilient, scalable, and maintainable systems. Whether you are developing a small application or a large distributed system, the Actor Model provides the tools and abstractions needed to manage complexity and ensure reliability.

## Features of the TodoListAkka Application
- **Task Management**: Add, update, and remove tasks.
- **Actor Hierarchy**: Each task is represented by its own actor, managed by a parent `TaskListActor`.
- **Test Coverage**: Comprehensive unit tests for all actors and scenarios.

---

## Step-by-Step Guide

### 1. **Getting Started**

#### Prerequisites
- [.NET SDK](https://dotnet.microsoft.com/download)
- Basic understanding of C# and Akka.NET

#### Clone the Repository
```bash
git clone https://github.com/your-username/TodoListAkka.git
cd TodoListAkka
```

### 2. Project Overview

Actor Model Structure  
	•	TaskActor: Represents an individual task.  
	•	TaskListActor: Manages a collection of TaskActor instances.  
	•	Messages: Defines communication between actors.

### 3. Core Components

`TaskActor`

Handles individual task behavior:  
	•	Messages:  
	•	UpdateTask: Updates the task’s description.  
	•	GetTaskState: Retrieves the current state of the task.

`TaskListActor`  

Manages the lifecycle of all tasks:  
	•	Messages:  
	•	AddTask: Adds a new task.  
	•	RemoveTask: Removes a task.  
	•	GetTasks: Fetches the list of all tasks.

#### Web API

The project implements a web API to interact with the actors. You can use the provided `.http` file for easy testing. The file is located at [src/TodoListAkka/Api/TodoListAkka.http](src/TodoListAkka/Api/TodoListAkka.http).

### 4. Run the Application

Build and Run

```bash
cd src/TodoListAkka
dotnet run
```

Output:

The application runs as an API demonstrating the actor hierarchy and task management.

### 5. Testing

Run Unit Tests

Navigate to the test project and execute:

```bash
cd test/TodoListAkka.Tests
dotnet test
```

Test Coverage

The tests cover:  
	•	Task creation, updates, and removal.  
	•	Parent-child actor relationships.  
	•	Handling invalid operations.  
    
### 6. Understanding the Benefits

Benefits of Using the Actor Model:
	1.	Isolation: Each actor encapsulates its state, preventing race conditions.
	2.	Concurrency: Actors process messages asynchronously, enabling high performance.
	3.	Fault Tolerance: Parent actors can restart or replace failed child actors.
	4.	Scalability: Actors can be distributed across nodes for horizontal scaling.

### Future Enhancements
	•	Add persistence using Akka.NET’s persistence module.
	•	Add support for distributed clustering.

### Contributing

Feel free to fork the repository and submit pull requests. Contributions are welcome!

### License

This project is licensed under the MIT License. See the LICENSE file for details.

### Next Steps

1. **Add Badges**:
   - Include badges for build status, test coverage, etc.
