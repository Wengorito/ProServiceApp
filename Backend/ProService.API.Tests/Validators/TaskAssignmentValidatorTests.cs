using ProService.API.Domain.Models.Employees;
using ProService.API.Domain.Models.Employees.Enums;
using ProService.API.Domain.Models.Tasks;
using ProService.API.Domain.Models.Tasks.Enums;
using ProService.API.Application.Validators;

namespace ProService.API.Tests.Validators;

public class TaskAssignmentValidatorTests
{
    private readonly TaskAssignmentValidator _validator;

    public TaskAssignmentValidatorTests()
    {
        _validator = new TaskAssignmentValidator();
    }

    [Fact]
    public void Validate_WithAlreadyAssignedTask_ThrowsException()
    {
        // Arrange
        var employee = new Employee { Id = 1 };
        var currentTasks = new List<ImplementationTask>();
        var newTasks = new List<ImplementationTask>
        {
            new() { Id = 1, AssigneeId = 5, Difficulty = 2, Type = TaskType.Implementation }
        };

        // Act & Assert
        var exception = Assert.Throws<Exception>(() => 
            _validator.Validate(employee, currentTasks, newTasks));
        Assert.Contains("already assigned", exception.Message);
    }

    [Fact]
    public void Validate_DeveloperWithNonImplementationTask_ThrowsException()
    {
        // Arrange
        var employee = new Employee { Id = 1, Role = EmployeeRole.Developer };
        var currentTasks = new List<TaskBase>();
        var newTasks = new List<TaskBase>
        {
            new DeploymentTask { Id = 1, AssigneeId = null, Difficulty = 2, Type = TaskType.Deployment }
        };

        // Act & Assert
        var exception = Assert.Throws<Exception>(() => 
            _validator.Validate(employee, currentTasks, newTasks));
        Assert.Contains("Developer can only have implementation tasks", exception.Message);
    }

    [Fact]
    public void Validate_WithTooFewTasks_ThrowsException()
    {
        // Arrange
        var employee = new Employee { Id = 1 };
        var currentTasks = new List<ImplementationTask>();
        var newTasks = new List<ImplementationTask>
        {
            new() { Id = 1, Difficulty = 2 },
            new() { Id = 2, Difficulty = 3 },
            new() { Id = 3, Difficulty = 2 }
        };

        // Act & Assert
        var exception = Assert.Throws<Exception>(() => 
            _validator.Validate(employee, currentTasks, newTasks));
        Assert.Contains("between 5 and 11 tasks", exception.Message);
    }

    [Fact]
    public void Validate_WithTooManyTasks_ThrowsException()
    {
        // Arrange
        var employee = new Employee { Id = 1, Role = EmployeeRole.DevOpsAdmin };
        var currentTasks = new List<TaskBase>();
        var newTasks = new List<TaskBase>
        {
            new DeploymentTask { Id = 1, Difficulty = 2 },
            new DeploymentTask { Id = 2, Difficulty = 2 },
            new DeploymentTask { Id = 3, Difficulty = 2 },
            new DeploymentTask { Id = 4, Difficulty = 2 },
            new DeploymentTask { Id = 5, Difficulty = 2 },
            new DeploymentTask { Id = 6, Difficulty = 2 },
            new DeploymentTask { Id = 7, Difficulty = 2 },
            new DeploymentTask { Id = 8, Difficulty = 2 },
            new DeploymentTask { Id = 9, Difficulty = 2 },
            new DeploymentTask { Id = 10, Difficulty = 2 },
            new DeploymentTask { Id = 11, Difficulty = 2 },
            new DeploymentTask { Id = 12, Difficulty = 2 }
        };

        // Act & Assert
        var exception = Assert.Throws<Exception>(() => 
            _validator.Validate(employee, currentTasks, newTasks));
        Assert.Contains("between 5 and 11 tasks", exception.Message);
    }

    [Fact]
    public void Validate_WithTooFewHardTasks_ThrowsException()
    {
        // Arrange
        var employee = new Employee { Id = 1, Role = EmployeeRole.DevOpsAdmin };
        var currentTasks = new List<TaskBase>();
        var newTasks = new List<TaskBase>
        {
            new DeploymentTask { Id = 1, Difficulty = 1 },
            new DeploymentTask { Id = 2, Difficulty = 2 },
            new DeploymentTask { Id = 3, Difficulty = 2 },
            new DeploymentTask { Id = 4, Difficulty = 2 },
            new DeploymentTask { Id = 5, Difficulty = 2 }
        };

        // Act & Assert
        var exception = Assert.Throws<Exception>(() => 
            _validator.Validate(employee, currentTasks, newTasks));
        Assert.Contains("Hard tasks must be between 10% and 30%", exception.Message);
    }

    [Fact]
    public void Validate_WithTooManyHardTasks_ThrowsException()
    {
        // Arrange
        var employee = new Employee { Id = 1, Role = EmployeeRole.DevOpsAdmin };
        var currentTasks = new List<TaskBase>();
        var newTasks = new List<TaskBase>
        {
            new DeploymentTask { Id = 1, Difficulty = 4 },
            new DeploymentTask { Id = 2, Difficulty = 4 },
            new DeploymentTask { Id = 3, Difficulty = 4 },
            new DeploymentTask { Id = 4, Difficulty = 4 },
            new DeploymentTask { Id = 5, Difficulty = 4 }
        };

        // Act & Assert
        var exception = Assert.Throws<Exception>(() => 
            _validator.Validate(employee, currentTasks, newTasks));
        Assert.Contains("Hard tasks must be between 10% and 30%", exception.Message);
    }

    [Fact]
    public void Validate_WithTooManyEasyTasks_ThrowsException()
    {
        // Arrange
        var employee = new Employee { Id = 1, Role = EmployeeRole.DevOpsAdmin };
        var currentTasks = new List<TaskBase>();
        var newTasks = new List<TaskBase>
        {
            new DeploymentTask { Id = 1, Difficulty = 1 },
            new DeploymentTask { Id = 2, Difficulty = 1 },
            new DeploymentTask { Id = 3, Difficulty = 1 },
            new DeploymentTask { Id = 4, Difficulty = 2 },
            new DeploymentTask { Id = 5, Difficulty = 2 },
            new DeploymentTask { Id = 6, Difficulty = 4 }
        };

        // Act & Assert
        var exception = Assert.Throws<Exception>(() => 
            _validator.Validate(employee, currentTasks, newTasks));
        Assert.Contains("Easy tasks cannot exceed 50%", exception.Message);
    }

    [Fact]
    public void Validate_WithValidTasks_DoesNotThrow()
    {
        // Arrange
        var employee = new Employee { Id = 1, Role = EmployeeRole.DevOpsAdmin };
        var currentTasks = new List<TaskBase>();
        var newTasks = new List<TaskBase>
        {
            new DeploymentTask { Id = 1, Difficulty = 1 },
            new DeploymentTask { Id = 2, Difficulty = 2 },
            new DeploymentTask { Id = 3, Difficulty = 2 },
            new DeploymentTask { Id = 4, Difficulty = 3 },
            new DeploymentTask { Id = 5, Difficulty = 4 },
            new DeploymentTask { Id = 6, Difficulty = 4 },
            new DeploymentTask { Id = 7, Difficulty = 3 },
            new DeploymentTask { Id = 8, Difficulty = 3 },
            new DeploymentTask { Id = 9, Difficulty = 3 },
            new DeploymentTask { Id = 10, Difficulty = 3 }
        };
        // 10 tasks: Easy=30% (1,2,3), Hard=20% (5,6) - Valid

        // Act & Assert - should not throw
        _validator.Validate(employee, currentTasks, newTasks);
    }

    [Fact]
    public void Validate_WithMixedCurrentAndNewTasks_ValidatesTotal()
    {
        // Arrange
        var employee = new Employee { Id = 1, Role = EmployeeRole.DevOpsAdmin };
        var currentTasks = new List<TaskBase>
        {
            new DeploymentTask { Id = 1, Difficulty = 1 },
            new DeploymentTask { Id = 2, Difficulty = 2 }
        };
        var newTasks = new List<TaskBase>
        {
            new DeploymentTask { Id = 3, Difficulty = 1 },
            new DeploymentTask { Id = 4, Difficulty = 3 },
            new DeploymentTask { Id = 5, Difficulty = 4 },
            new DeploymentTask { Id = 6, Difficulty = 4 },
            new DeploymentTask { Id = 7, Difficulty = 3 },
            new DeploymentTask { Id = 8, Difficulty = 3 },
            new DeploymentTask { Id = 9, Difficulty = 3 },
            new DeploymentTask { Id = 10, Difficulty = 3 }
        };
        // Total 10 tasks: Easy=30% (1,2,3), Hard=20% (5,6) - Valid

        // Act & Assert - should not throw (total = 10 tasks)
        _validator.Validate(employee, currentTasks, newTasks);
    }

    [Fact]
    public void Validate_DevOpsAdminWithVariousTaskTypes_Succeeds()
    {
        // Arrange
        var employee = new Employee { Id = 1, Name = "Jane", Role = EmployeeRole.DevOpsAdmin };
        var currentTasks = new List<TaskBase>();
        var newTasks = new List<TaskBase>
        {
            new DeploymentTask { Id = 1, Difficulty = 1 },
            new DeploymentTask { Id = 2, Difficulty = 1 },
            new ImplementationTask { Id = 3, Difficulty = 2 },
            new MaintenanceTask { Id = 4, Difficulty = 3 },
            new DeploymentTask { Id = 5, Difficulty = 4 },
            new DeploymentTask { Id = 6, Difficulty = 4 },
            new DeploymentTask { Id = 7, Difficulty = 3 },
            new DeploymentTask { Id = 8, Difficulty = 3 },
            new DeploymentTask { Id = 9, Difficulty = 3 },
            new DeploymentTask { Id = 10, Difficulty = 3 }
        };
        // Total 10 tasks: Easy=30% (1,2,3), Hard=20% (5,6) - Valid

        // Act & Assert - should not throw
        _validator.Validate(employee, currentTasks, newTasks);
    }
}
