using System.Text.Json;
using System.IO;
using System.Security.Cryptography.X509Certificates;

var fileName = "task.json";

if(args.Length == 0 || args.Length > 3)
{
    Console.WriteLine("Wrong usage. Use the command \"help\" for more information.");
    return; 
}
else if(args[0] == "help")
{
    Console.WriteLine("Usage:");
    Console.WriteLine("1. add <description> - Adds a new task with the given description.");
    Console.WriteLine("2. list - Lists all tasks.");
    Console.Write("3. list <description>  - Lists all tasks with the given description");
    Console.WriteLine(" (todo, in-progress, done).");
    Console.WriteLine("5. update <id> <description> - Updates the description of the task with the given id.");
    Console.WriteLine("6. mark-in-progress <id>  - Marks the task with the given id as in progress.");
    Console.WriteLine("7. mark-done <id>  - Marks the task with the given id as done.");
    Console.WriteLine("8. delete <id>  - Deletes the task with the given id.");
}
else if (args[0] == "add" && args.Length == 2) 
{
    //new task
    Task task = new Task(args[1], Status.todo);

    //new list of tasks
    List<Task> tasks = new();

    string jsonString;     
    bool exists = File.Exists(fileName);
    if (!exists)
    {   
        //add task to the list
        tasks.Add(task);
        //serialize to Json
        jsonString = JsonSerializer.Serialize(tasks);      
        //write the file
        File.WriteAllText(fileName, jsonString); 
        Console.WriteLine($"Task added succesfully (ID: {task.Id})");
    } else
    {   
        //read the file
        string resultsJson = File.ReadAllText(fileName);  
        //deserialize the file
        List<Task>? results = JsonSerializer.Deserialize<List<Task>>(resultsJson);
        if(results == null)
        {
            task.Id = 1;
        }
        else
        {
            int lastId = results.Last().Id;
            task.Id = lastId + 1;
        }
        //add task to the list
        results.Add(task);
        //serialize to Json
        jsonString = JsonSerializer.Serialize(results);
        //write the file
        File.WriteAllText(fileName, jsonString);
    }

} else if (args[0] == "list" && args.Length <= 1)
{   
    string resultsJson = File.ReadAllText(fileName);  
    List<Task>? results = JsonSerializer.Deserialize<List<Task>>(resultsJson);
    if(results == null || results.Count == 0)
    {
        Console.WriteLine("No tasks found.");
    }
    else
    {
        foreach (var item in results)
        {
            Console.WriteLine($"Id: {item.Id}. Description: {item.Description}");    
        }
    }
    
} else if (args[0] == "list" && args.Length == 2)
{   
    string resultsJson = File.ReadAllText(fileName);  
    List<Task>? results = JsonSerializer.Deserialize<List<Task>>(resultsJson);
    
    if(args[1] == "todo")
    {
        var todoTasks = results.Where(x => x.Status == Status.todo).ToList();
        if(results.Count == 0 || todoTasks.Count == 0)
        {
            Console.WriteLine("No tasks found.");
        }
        else
        {
            foreach (var item in todoTasks)
            {
                Console.WriteLine($"Id: {item.Id}. Description: {item.Description}");    
            }
        }   
    } else if(args[1] == "in-progress")
    {
        var inProgressTasks = results.Where(x => x.Status == Status.in_progress).ToList();
        if(results.Count == 0 || inProgressTasks.Count == 0)
        {
            Console.WriteLine("No tasks found.");
        }
        else
        {
            foreach (var item in inProgressTasks)
            {
                Console.WriteLine($"Id: {item.Id}. Description: {item.Description}");    
            }
        }
    } else if(args[1] == "done")
    {
        var doneTasks = results.Where(x => x.Status == Status.done).ToList();
        if(results.Count == 0 || doneTasks.Count == 0)
        {
            Console.WriteLine("No tasks found.");
        }
        else
        {
            foreach (var item in doneTasks)
            {
                Console.WriteLine($"Id: {item.Id}. Description: {item.Description}");    
            }
        }
    }
} else if (args[0] == "delete" && args.Length == 2)
{
    string resultsJson = File.ReadAllText(fileName);  
    List<Task>? results = JsonSerializer.Deserialize<List<Task>>(resultsJson);
    int deleteId = Int32.Parse(args[1]);
    Task find = results.FirstOrDefault(x => x.Id == deleteId);

    if (results != null && find != null)
    {   
        results.Remove(find);
        Console.WriteLine($"Task deleted successfully (ID: {args[1]})");
        //serialize to Json
        string jsonString = JsonSerializer.Serialize(results);
        //write the file
        File.WriteAllText(fileName, jsonString);
    }
    else
    {
        Console.WriteLine($"No task with Id: {args[1]}");
    }
} else if (args[0] == "update" && args.Length == 3)
{
    string resultsJson = File.ReadAllText(fileName);  
    List<Task>? results = JsonSerializer.Deserialize<List<Task>>(resultsJson);

    int updateId = Int32.Parse(args[1]);
    Task find = results.FirstOrDefault(x => x.Id == updateId);
    
    if (results != null && find != null)
    {   
        find.Description = args[2];
        find.UpdatedAt = DateTime.Now;
        Console.WriteLine($"Task updated successfully (ID: {find.Id})");
        Console.WriteLine($"Task updated created at {find.CreatedAt})");
        Console.WriteLine($"Task updated updated at {find.UpdatedAt})");

        //serialize to Json
        string jsonString = JsonSerializer.Serialize(results);
        //write the file
        File.WriteAllText(fileName, jsonString);
    }
    else
    {
        Console.WriteLine($"No task with Id: {args[1]}");
    }
} else if(args[0] == "mark-in-progress" || args[0] == "mark-done" && args.Length == 2)
{
    string resultsJson = File.ReadAllText(fileName);  
    List<Task>? results = JsonSerializer.Deserialize<List<Task>>(resultsJson);   
    int markId = Int32.Parse(args[1]);
    Task find = results.FirstOrDefault(x => x.Id == markId);

    if (args[0] == "mark-in-progress" && find != null)
    {   
        if (find.Status == Status.in_progress)
        {
            Console.WriteLine($"Task: {find.Description} is already marked as in progress.");
        }
        find.Status = Status.in_progress;
        find.UpdatedAt = DateTime.Now;
    } else if(args[0] == "mark-done" && find != null)
    {   
        if(find.Status == Status.done)
        {
            Console.WriteLine($"Task: {find.Description} is already marked as done.");
        }
        find.Status = Status.done;
        find.UpdatedAt = DateTime.Now;
    }
        //serialize to Json
        string jsonString = JsonSerializer.Serialize(results);
        //write the file
        File.WriteAllText(fileName, jsonString);
    
    Console.WriteLine($"Task: {find.Description} marked as {find.Status}");

} else
{
    Console.WriteLine("Wrong usage. Use the command \"help\" for more information.");
}