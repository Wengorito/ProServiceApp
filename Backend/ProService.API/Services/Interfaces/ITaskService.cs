using ProService.API.Models.Tasks;

namespace ProService.API.Services.Interfaces;

public interface ITaskService
{
    Task<List<TaskBase>> GetAvailableTasksAsync(int page, int pageSize = 10);
    Task<List<TaskBase>> GetAssignedTasksAsync(int employeeId, int page, int pageSize);
    Task AssignTasks(IEnumerable<int> taskIds, int employeeId);
}