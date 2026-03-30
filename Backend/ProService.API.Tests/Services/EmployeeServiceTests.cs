using Moq;
using ProService.API.Models.Employees;
using ProService.API.Models.Enums;
using ProService.API.Repository;
using ProService.API.Services;

namespace ProService.API.Tests.Services;

public class EmployeeServiceTests
{
    private readonly Mock<IMockRepository> _mockRepository;
    private readonly EmployeeService _employeeService;

    public EmployeeServiceTests()
    {
        _mockRepository = new Mock<IMockRepository>();
        _employeeService = new EmployeeService(_mockRepository.Object);
    }

    [Fact]
    public async Task GetEmployeesAsync_WithEmployees_ReturnsEmployeeDtos()
    {
        // Arrange
        var employees = new List<Employee>
        {
            new() { Id = 1, Name = "John Doe", Role = EmployeeRole.Developer },
            new() { Id = 2, Name = "Jane Smith", Role = EmployeeRole.DevOpsAdmin }
        };

        _mockRepository.Setup(r => r.GeEmployeesAsync()).ReturnsAsync(employees);

        // Act
        var result = await _employeeService.GetEmployeesAsync();

        // Assert
        var employeeList = result.ToList();
        Assert.Equal(2, employeeList.Count);
        Assert.Contains(employeeList, e => e.Name == "John Doe" && e.Role == "Developer");
        Assert.Contains(employeeList, e => e.Name == "Jane Smith" && e.Role == "DevOpsAdmin");
    }

    [Fact]
    public async Task GetEmployeesAsync_WithNoEmployees_ReturnsEmptyList()
    {
        // Arrange
        _mockRepository.Setup(r => r.GeEmployeesAsync()).ReturnsAsync(new List<Employee>());

        // Act
        var result = await _employeeService.GetEmployeesAsync();

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetEmployeesAsync_CallsRepositoryOnce()
    {
        // Arrange
        _mockRepository.Setup(r => r.GeEmployeesAsync()).ReturnsAsync(new List<Employee>());

        // Act
        await _employeeService.GetEmployeesAsync();

        // Assert
        _mockRepository.Verify(r => r.GeEmployeesAsync(), Times.Once);
    }

    [Fact]
    public async Task GetEmployeesAsync_MapsEmployeeRolesToStrings()
    {
        // Arrange
        var employees = new List<Employee>
        {
            new() { Id = 1, Name = "Test", Role = EmployeeRole.Developer },
            new() { Id = 2, Name = "Test2", Role = EmployeeRole.DevOpsAdmin }
        };

        _mockRepository.Setup(r => r.GeEmployeesAsync()).ReturnsAsync(employees);

        // Act
        var result = await _employeeService.GetEmployeesAsync();

        // Assert
        var employeeList = result.ToList();
        Assert.All(employeeList, e => Assert.NotNull(e.Role));
    }
}
