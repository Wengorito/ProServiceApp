using Moq;
using ProService.API.Models.Employees;
using ProService.API.Models.Enums;
using ProService.API.Models.Tasks;
using ProService.API.Models.Tasks.Enums;
using ProService.API.Repository;
using ProService.API.Services;
using ProService.API.Validators;
using Xunit;
using TaskStatus = ProService.API.Models.Tasks.Enums.TaskStatus;

namespace ProService.API.Tests.Services;

public class TaskAssignmentServiceTests
{
    private readonly Mock<IMockRepository> _mockRepository;
    private readonly Mock<ITaskAssignmentValidator> _mockValidator;
    private readonly TaskAssignmentService _taskAssignmentService;

    public TaskAssignmentServiceTests()
    {
        _mockRepository = new Mock<IMockRepository>();
        _mockValidator = new Mock<ITaskAssignmentValidator>();
        _taskAssignmentService = new TaskAssignmentService(_mockRepository.Object, _mockValidator.Object);
    }

    [Fact]
    public async Task GetAvailableTasksAsync_WithTasks_ReturnTaskDtos()
    {
        // Arrange
        var tasks = new List<ImplementationTask>
        {
            new() { Id = 1, Overview = "Task 1", Difficulty = 2, Type = TaskType.Implementation, Status = ProService.API.Models.Tasks.Enums.TaskStatus.ToDo },
            new() { Id = 2, Overview = "Task 2", Difficulty = 3, Type = TaskType.Implementation, Status = ProService.API.Models.Tasks.Enums.TaskStatus.ToDo }
        };

        _mockRepository.Setup(r => r.GetAvailableTasksAsync(1, 10)).ReturnsAsync(tasks);

        // Act
        var result = await _taskAssignmentService.GetAvailableTasksAsync(1, 10);

        // Assert
        var taskList = result.ToList();
        Assert.Equal(2, taskList.Count);
    }

    // [Fact]
    // public async Task GetAvailableTasksAsync_CallsRepositoryWithCorrectParameters()
    // {
    //     // Arrange
    //     _mockRepository.Setup(r => r.GetAvailableTasksAsync(2, 5)).ReturnsAsync(new List<TaskBase>());

    //     // Act
    //     await _taskAssignmentService.GetAvailableTasksAsync(2, 5);

    //     // Assert
    //     _mockRepository.Verify(r => r.GetAvailableTasksAsync(2, 5), Times.Once);
    // }

    [Fact]
    public async Task GetAssignedTasksAsync_WithValidEmployee_ReturnsTasks()
    {
        // Arrange
        var employeeId = 1;
        var employee = new Employee { Id = employeeId, Name = "John", Role = EmployeeRole.Developer };
        var tasks = new List<ImplementationTask>
        {
            new() { Id = 1, Overview = "Task 1", Difficulty = 2, Type = TaskType.Implementation, Status = TaskStatus.ToDo, AssigneeId = employeeId }
        };

        _mockRepository.Setup(r => r.GetEmployeeAsync(employeeId)).ReturnsAsync(employee);
        _mockRepository.Setup(r => r.GetAssignedTasksAsync(employeeId, 1, 10)).ReturnsAsync(tasks);

        // Act
        var result = await _taskAssignmentService.GetAssignedTasksAsync(employeeId, 1, 10);

        // Assert
        var taskList = result.ToList();
        Assert.Single(taskList);
    }

    [Fact]
    public async Task GetAssignedTasksAsync_WithNonexistentEmployee_ThrowsException()
    {
        // Arrange
        _mockRepository.Setup(r => r.GetEmployeeAsync(It.IsAny<int>())).ReturnsAsync((Employee?)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => 
            _taskAssignmentService.GetAssignedTasksAsync(999, 1, 10));
        Assert.Contains("Employee not found", exception.Message);
    }

    [Fact]
    public async Task AssignTasks_WithValidEmployeeAndTasks_CallsValidator()
    {
        // Arrange
        var employeeId = 1;
        var taskIds = new[] { 1, 2, 3 };
        var employee = new Employee { Id = employeeId, Name = "John", Role = EmployeeRole.Developer };
        var newTasks = new List<ImplementationTask>
        {
            new() { Id = 1, Difficulty = 2 },
            new() { Id = 2, Difficulty = 3 },
            new() { Id = 3, Difficulty = 2 }
        };
        var currentTasks = new List<ImplementationTask>();

        _mockRepository.Setup(r => r.GetEmployeeAsync(employeeId)).ReturnsAsync(employee);
        _mockRepository.Setup(r => r.GetTasksAsync(taskIds)).ReturnsAsync(newTasks);
        _mockRepository.Setup(r => r.GetEmployeeTasksAsync(employeeId)).ReturnsAsync(currentTasks);

        // Act
        await _taskAssignmentService.AssignTasks(taskIds, employeeId);

        // Assert
        _mockValidator.Verify(v => v.Validate(It.IsAny<Employee>(), It.IsAny<IReadOnlyList<TaskBase>>(), It.IsAny<IReadOnlyList<TaskBase>>()), Times.Once);
    }

    [Fact]
    public async Task AssignTasks_WithNonexistentEmployee_ThrowsException()
    {
        // Arrange
        _mockRepository.Setup(r => r.GetEmployeeAsync(It.IsAny<int>())).ReturnsAsync((Employee?)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => 
            _taskAssignmentService.AssignTasks(new[] { 1, 2 }, 999));
        Assert.Contains("No employee by given ID found.", exception.Message);
    }

    [Fact]
    public async Task AssignTasks_WithNoTasksOfIds_ThrowsException()
    {
        // Arrange
        var employeeId = 1;
        _mockRepository.Setup(r => r.GetEmployeeAsync(employeeId)).ReturnsAsync(new Employee());
        _mockRepository.Setup(r => r.GetTasksAsync(It.IsAny<List<int>>())).ReturnsAsync((new List<TaskBase>()));

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => 
            _taskAssignmentService.AssignTasks(new[] { 1, 2 }, 1));
        Assert.Contains("No tasks by given IDs found.", exception.Message);
    }

    [Fact]
    public async Task AssignTasks_WithValidData_CallsRepositoryAssignTasks()
    {
        // Arrange
        var employeeId = 1;
        var taskIds = new[] { 1, 2, 3 };
        var employee = new Employee { Id = employeeId, Name = "John", Role = EmployeeRole.Developer };
        var newTasks = new List<ImplementationTask>
        {
            new() { Id = 1, Difficulty = 2 },
            new() { Id = 2, Difficulty = 3 },
            new() { Id = 3, Difficulty = 2 }
        };
        var currentTasks = new List<ImplementationTask>();

        _mockRepository.Setup(r => r.GetEmployeeAsync(employeeId)).ReturnsAsync(employee);
        _mockRepository.Setup(r => r.GetTasksAsync(taskIds)).ReturnsAsync(newTasks);
        _mockRepository.Setup(r => r.GetEmployeeTasksAsync(employeeId)).ReturnsAsync(currentTasks);

        // Act
        await _taskAssignmentService.AssignTasks(taskIds, employeeId);

        // Assert
        _mockRepository.Verify(r => r.AssignTasks(taskIds, employeeId), Times.Once);
    }
}
