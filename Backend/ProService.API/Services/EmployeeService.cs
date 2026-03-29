using ProService.API.DTOs.Employees;
using ProService.API.Repository;
using ProService.API.Services.Interfaces;

namespace ProService.API.Services;

public class EmployeeService(IMockRepository mockRepository) : IEmployeeService
{
    private readonly IMockRepository _mockRepository = mockRepository;

    public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync()
    {
        var employees = await _mockRepository.GeEmployeesAsync();
        var dtos = new List<EmployeeDto>();

        foreach(var employee in employees)
        {
            dtos.Add(new EmployeeDto(employee.Id, employee.Name, employee.Role.ToString()));
        }

        return dtos;
    }
}
