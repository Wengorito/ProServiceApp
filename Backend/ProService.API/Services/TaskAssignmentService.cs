using ProService.API.DTOs.Tasks;
using ProService.API.Repository;
using ProService.API.Services.Interfaces;

namespace ProService.API.Services;

public class TaskAssignmentService(IMockRepository mockRepository) : ITaskAssignmentService
{
    private readonly IMockRepository _mockRepository = mockRepository;

    public async Task<IEnumerable<TaskDto>> GetAvailableTasksAsync(int pageNumber, int pageSize)
    {
        ValidatePagination(ref pageNumber, ref pageSize);

        return (await _mockRepository.GetAvailableTasksAsync(pageNumber, pageSize)).Select(t => new TaskDto(t));
    }

    public async Task<IEnumerable<TaskDto>> GetAssignedTasksAsync(int employeeId, int pageNumber, int pageSize)
    {
        ValidatePagination(ref pageNumber, ref pageSize);
        
        // czytelniejszy zapis
        var tasks = await _mockRepository.GetAssignedTasksAsync(employeeId, pageNumber, pageSize);
        return tasks.Select(t => new TaskDto(t));
    }

    public async Task AssignTasks(IEnumerable<int> taskIds, int employeeId)
    {
        // perform buisness rules validation
        await _mockRepository.AssignTasks(taskIds, employeeId);
    }

    private static void ValidatePagination(ref int pageNumber, ref int pageSize)
    {
        if (pageNumber < 1)
        {
            throw new ArgumentException($"Page number must be integer greater than 0.");
        }

        if (pageSize < 1)
        {
            throw new ArgumentException($"Page size must be integer greater than 0.");
        }

        if (pageSize > Commons.ApiConstants.MAX_TASK_PAGE_SIZE)
        {
            throw new ArgumentException($"Max page size is {Commons.ApiConstants.MAX_TASK_PAGE_SIZE}");
        }
    }
}