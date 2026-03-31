using ProService.API.Domain.Models.Employees;
using ProService.API.Domain.Models.Tasks;

namespace ProService.API.Domain.Repository;

public interface IMockRepository
{
    Task<IEnumerable<Employee>> GeEmployeesAsync();
    Task<Employee?> GetEmployeeAsync(int id);
    Task<IEnumerable<TaskBase>> GetTasksAsync(IEnumerable<int> ids);
    Task<IEnumerable<TaskBase>> GetEmployeeTasksAsync(int employeeId);
    Task<IEnumerable<TaskBase>> GetAvailableTasksAsync(int page, int pageSize);
    Task<IEnumerable<TaskBase>> GetAssignedTasksAsync(int employeeId, int page, int pageSize);
    Task AssignTasks(IEnumerable<int> tasksIds, int employeeId);
}