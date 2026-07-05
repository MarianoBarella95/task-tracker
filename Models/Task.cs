public enum Status
{
    todo, 
    in_progress,
    done
}

public class Task
{
    public int Id { get; set; }
    public string Description { get; set; }
    public Status Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public Task(string description, Status status)
    {
        this.Id = Id;
        this.Description = description; 
        this.Status = status; 
        this.CreatedAt = DateTime.Now;
        this.UpdatedAt = DateTime.Now;
    }
}