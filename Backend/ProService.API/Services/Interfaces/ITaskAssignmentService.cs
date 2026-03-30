using ProService.API.DTOs.Tasks.Responses;

namespace ProService.API.Services.Interfaces;

public interface ITaskAssignmentService
{
    Task<IEnumerable<TaskDto>> GetAvailableTasksAsync(int pageNumber, int pageSize);
    Task<IEnumerable<TaskDto>> GetAssignedTasksAsync(int employeeId, int pageNumber, int pageSize);
    Task AssignTasks(IEnumerable<int> taskIds, int employeeId);
}