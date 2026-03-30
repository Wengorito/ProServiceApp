using ProService.API.DTOs.Tasks.Responses;
using ProService.API.Repository;
using ProService.API.Services.Interfaces;
using ProService.API.Validators;

namespace ProService.API.Services;

public class TaskAssignmentService(IMockRepository mockRepository, ITaskAssignmentValidator validator) : ITaskAssignmentService
{
    private readonly IMockRepository _mockRepository = mockRepository;
    private readonly ITaskAssignmentValidator _validator = validator;

    public async Task<IEnumerable<TaskDto>> GetAvailableTasksAsync(int pageNumber, int pageSize)
    {
        return (await _mockRepository.GetAvailableTasksAsync(pageNumber, pageSize)).Select(t => new TaskDto(t));
    }

    public async Task<IEnumerable<TaskDto>> GetAssignedTasksAsync(int employeeId, int pageNumber, int pageSize)
    {
        var employee = await _mockRepository.GetEmployeeAsync(employeeId);
        if (employee == null)
        {
            throw new Exception($"Employee not found (id: {employeeId})");
        }
        
        // czytelniejszy zapis
        var tasks = await _mockRepository.GetAssignedTasksAsync(employeeId, pageNumber, pageSize);
        return tasks.Select(t => new TaskDto(t));
    }

    public async Task AssignTasks(IEnumerable<int> taskIds, int employeeId)
    {
        var employee = await _mockRepository.GetEmployeeAsync(employeeId);
        if (employee == null)
        {
            throw new Exception("No employee by given ID found.");
        }

        var currentTasks = (await _mockRepository.GetEmployeeTasksAsync(employeeId)).ToList();

        var newTasks = (await _mockRepository.GetTasksAsync(taskIds)).ToList();
        if (newTasks.Count == 0)
        {
            throw new Exception($"No tasks by given IDs found.");
        }

        _validator.Validate(employee, currentTasks, newTasks);

        await _mockRepository.AssignTasks(taskIds, employeeId);
    }
}