using ProService.API.Models.Employees;
using ProService.API.Repository;
using ProService.API.Services.Interfaces;

namespace ProService.API.Services;

public class EmployeeService(IMockRepository mockRepository) : IEmployeeService
{
    private readonly IMockRepository _mockRepository = mockRepository;

    public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
    {
        return await _mockRepository.GetAllEmployeesAsync();
    }
}
