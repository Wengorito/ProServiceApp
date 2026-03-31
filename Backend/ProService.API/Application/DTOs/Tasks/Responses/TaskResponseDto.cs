using ProService.API.Domain.Models.Tasks;

namespace ProService.API.Application.DTOs.Tasks.Responses;

public record TaskResponseDto(int Id, string Overview, int Difficulty, string Type, string Status)
{
    public TaskResponseDto(TaskBase task) : this (
        task.Id,
        task.Overview,
        task.Difficulty,
        task.Type.ToString(),
        task.Status.ToString())
    {
    }
}