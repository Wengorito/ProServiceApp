using ProService.API.Models.Employees;
using ProService.API.Models.Tasks;

namespace ProService.API.Repository;

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