using ProService.API.DTOs.Tasks;
using ProService.API.Models.Tasks;
using ProService.API.Repository;
using ProService.API.Services.Interfaces;

namespace ProService.API.Services;

public class TaskService(IMockRepository mockRepository) : ITaskService
{
    private readonly IMockRepository _mockRepository = mockRepository;

    public async Task<IEnumerable<TaskDto>> GetAvailableTasksAsync(int page, int pageSize)
    {
        return (await _mockRepository.GetAvailableTasksAsync(page, pageSize)).Select(t => new TaskDto(t));
    }

    public async Task<IEnumerable<TaskDto>> GetAssignedTasksAsync(int employeeId, int page, int pageSize)
    {
        // czytelniejszy zapis
        var tasks = await _mockRepository.GetAssignedTasksAsync(employeeId, page, pageSize);
        return tasks.Select(t => new TaskDto(t));
    }

    public async Task AssignTasks(IEnumerable<int> taskIds, int employeeId)
    {
        // perform buisness rules validation
        await _mockRepository.AssignTasks(taskIds, employeeId);
    }
}