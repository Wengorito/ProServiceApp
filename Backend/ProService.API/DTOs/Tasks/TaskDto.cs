using ProService.API.Models.Tasks;
using Enums = ProService.API.Models.Tasks.Enums;

namespace ProService.API.DTOs.Tasks;

public record TaskDto(int Id, string Overview, int Difficulty, string Type, string Status)
{
    public TaskDto(TaskBase task) : this (
        task.Id,
        task.Overview,
        task.Difficulty,
        task.Type.ToString(),
        task.Status.ToString())
    {
    }
}