using System.ComponentModel.DataAnnotations;

namespace ProService.API.Models.Tasks;

public abstract class TaskBase
{
    public int Id;

    [MaxLength(100)]
    public string Overview { get; set; } = string.Empty;

    [Range(1, 5)]
    public int Difficulty { get; set; }

    public Enums.TaskType Type { get; set; }

    public Enums.TaskStatus Status { get; set; } = Enums.TaskStatus.ToDo;

    public int? AssigneeId { get; set; }
}