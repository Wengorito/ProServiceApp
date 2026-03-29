using ProService.API.Models.Employees;
using ProService.API.Models.Tasks;

namespace ProService.API.Repository;

public interface IMockRepository
{
    Task<IEnumerable<Employee>> GetAllEmployeesAsync();
    Task<IEnumerable<TaskBase>> GetAvailableTasksAsync(int page, int pageSize);
    Task<IEnumerable<TaskBase>> GetEmployeeAssignedTasksAsync(int page, int pageSize, int employeeId);
}