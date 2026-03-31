using ProService.API.DTOs.Tasks.Responses;

namespace ProService.API.Services.Interfaces;

public interface ITaskAssignmentService
{
    Task<IEnumerable<TaskResponseDto>> GetAvailableTasksAsync(int pageNumber, int pageSize);
    Task<IEnumerable<TaskResponseDto>> GetAssignedTasksAsync(int employeeId, int pageNumber, int pageSize);
    Task AssignTasks(IEnumerable<int> taskIds, int employeeId);
}