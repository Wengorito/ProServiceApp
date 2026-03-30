using ProService.API.DTOs.Tasks;

namespace ProService.API.Services.Interfaces;

public interface ITaskAssignmentService
{
    Task<IEnumerable<TaskDto>> GetAvailableTasksAsync(int page, int pageSize);
    Task<IEnumerable<TaskDto>> GetAssignedTasksAsync(int employeeId, int page, int pageSize);
    Task AssignTasks(IEnumerable<int> taskIds, int employeeId);
}