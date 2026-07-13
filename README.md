# Task Tracker

This is Task Tracker, a CLI application that allows you to maintain a lists of tasks, and perform CRUD operations on them. This project is part of the Backend Roadmap in roadmap.sh [https://roadmap.sh/projects/task-tracker].

## Quicstart
First of all, you must have installed the .NET SDK. You can download it from here https://dotnet.microsoft.com/es-es/download

- Clone the project: `git clone https://github.com/MarianoBarella95/task-tracker.git`
- Go to the project folder: `cd task-tracker`
- Run the project. For example: `dotnet run add "task"` 

## Commands
Introducing the command "help", the program will display in the console a list with all the commands available, and the correct usage for the application,
as shown below: 

+ add \<description\> - Adds a new task with the given description.
+ list - Lists all tasks.
+ list \<description\> - Lists all tasks with the given status. (todo, in-progress-done).
+ update \<id\> \<description\> - Updates the description of the task with the given id.
+ mark-in-progress \<id\> - Marks the task with the given id as in progress.
+ mark-done \<id\> - Marks the task with the given id as done.
+ delete \<id\>  - Deletes the task with the given id.
