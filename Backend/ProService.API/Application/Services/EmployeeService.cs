using ProService.API.Application.DTOs.Employees;
using ProService.API.Domain.Repository;
using ProService.API.Application.Services.Interfaces;

namespace ProService.API.Application.Services;

public class EmployeeService(IMockRepository mockRepository) : IEmployeeService
{
    private readonly IMockRepository _mockRepository = mockRepository;

    public async Task<IEnumerable<EmployeeResponseDto>> GetEmployeesAsync()
    {
        var employees = await _mockRepository.GeEmployeesAsync();
        var dtos = new List<EmployeeResponseDto>();

        foreach(var employee in employees)
        {
            dtos.Add(new EmployeeResponseDto(employee.Id, employee.Name, employee.Role.ToString()));
        }

        return dtos;
    }
}
