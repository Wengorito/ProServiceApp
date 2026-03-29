using ProService.API.DTOs.Tasks;
using ProService.API.Models.Tasks;

namespace ProService.API.Services.Interfaces;

public interface ITaskService
{
    Task<IEnumerable<TaskDto>> GetAvailableTasksAsync(int page, int pageSize = 10);
    Task<IEnumerable<TaskDto>> GetAssignedTasksAsync(int employeeId, int page, int pageSize);
    Task AssignTasks(IEnumerable<int> taskIds, int employeeId);
}