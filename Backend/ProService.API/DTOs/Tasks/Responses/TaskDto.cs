using ProService.API.Models.Tasks;

namespace ProService.API.DTOs.Tasks.Responses;

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