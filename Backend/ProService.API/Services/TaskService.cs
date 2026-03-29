using ProService.API.Models.Tasks;
using ProService.API.Repository;
using ProService.API.Services.Interfaces;

namespace ProService.API.Services;

public class TaskService(IMockRepository mockRepository) : ITaskService
{
    private readonly IMockRepository _mockRepository = mockRepository;

    public async Task<List<TaskBase>> GetAvailableTasksAsync(int page, int pageSize)
    {
        return (await _mockRepository.GetAvailableTasksAsync(page, pageSize)).ToList();
    }

    public async Task<List<TaskBase>> GetAssignedTasksAsync(int employeeId, int page, int pageSize)
    {
        return (await _mockRepository.GetAssignedTasksAsync(employeeId, page, pageSize)).ToList();
    }

    public async Task AssignTasks(IEnumerable<int> taskIds, int employeeId)
    {
        // perform buisness rules validation
        await _mockRepository.AssignTasks(taskIds, employeeId);
    }
}