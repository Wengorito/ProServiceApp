namespace ProService.API.Models.Tasks;

public abstract class TaskBase
{
    public int Id;
    public string Overview { get; set; } = string.Empty;
    public int Difficulty { get; set; }
    public Enums.TaskType TaskType { get; set; }
    public Enums.TaskStatus Status { get; set; }
    public int? AssignedUserId { get; set; }
}