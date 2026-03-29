using ProService.API.Models.Employees;

namespace ProService.API.Services.Interfaces;

public interface IEmployeeService
{
    Task<IEnumerable<Employee>> GetAllEmployeesAsync();
}