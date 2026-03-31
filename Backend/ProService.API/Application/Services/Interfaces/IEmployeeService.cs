using ProService.API.Application.DTOs.Employees;

namespace ProService.API.Application.Services.Interfaces;

public interface IEmployeeService
{
    Task<IEnumerable<EmployeeResponseDto>> GetEmployeesAsync();
}