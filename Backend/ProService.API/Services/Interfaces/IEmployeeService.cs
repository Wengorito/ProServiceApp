using ProService.API.DTOs.Employees;

namespace ProService.API.Services.Interfaces;

public interface IEmployeeService
{
    Task<IEnumerable<EmployeeDto>> GetEmployeesAsync();
}