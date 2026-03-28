using System.ComponentModel.DataAnnotations;

namespace ProService.API.Models.Tasks;

public abstract class TaskBase
{
    public int Id;

    [MaxLength(100)]
    public required string Overview { get; set; }

    [Range(1, 5)]
    public required int Difficulty { get; set; }

    public required Enums.TaskType TaskType { get; set; }

    public required Enums.TaskStatus Status { get; set; }

    public int? AssignedUserId { get; set; }
}