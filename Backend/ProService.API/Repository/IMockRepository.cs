using ProService.API.Models.Employees;
using ProService.API.Models.Tasks;

namespace ProService.API.Repository;

public interface IMockRepository
{
    Task<IEnumerable<Employee>> GeEmployeesAsync();
    Task<IEnumerable<TaskBase>> GetAvailableTasksAsync(int page, int pageSize);
    Task<IEnumerable<TaskBase>> GetAssignedTasksAsync(int employeeId, int page, int pageSize);
    Task AssignTasks(IEnumerable<int> tasksIds, int employeeId);
}