using ProService.API.Models.Employees;
using ProService.API.Models.Enums;
using ProService.API.Models.Tasks;

namespace ProService.API.Repository;

public class MockRepository : IMockRepository
{
    private readonly List<Employee> _employees;
    private readonly List<TaskBase> _tasks;

    public MockRepository()
    {
        _employees = new List<Employee>
        {
            new() { Id = 1, Name = "Krzysztof (Dev)", Role = EmployeeRole.Developer },
            new() { Id = 2, Name = "Zenek (Dev)", Role = EmployeeRole.Developer },
            new() { Id = 3, Name = "Hania (DevOps)", Role = EmployeeRole.DevOps },
            new() { Id = 4, Name = "Marcin (DevOps)", Role = EmployeeRole.DevOps },
            new() { Id = 5, Name = "Ewelina (DevOps)", Role = EmployeeRole.DevOps }
        };

        _tasks = new List<TaskBase>();

        for (int i = 1; i <= 10; i++)
        {
            _tasks.Add(new ImplementationTask
            {
                Id = i,
                Overview = $"Implementation Task {i}",
                Difficulty = (i % 5) + 1,
                ImplementationDetails = $"Details for task {i}",
            });
        }

        for (int i = 11; i <= 20; i++)
        {
            _tasks.Add(new DeploymentTask
            {
                Id = i,
                Overview = $"Deployment Task {i}",
                Difficulty = (i % 5) + 1,
                Deadline = DateOnly.FromDateTime(DateTime.Now.AddDays(7)),
                DeploymentScope = $"Scope {i}",
            });
        }

        for (int i = 21; i <= 30; i++)
        {
            _tasks.Add(new MaintenanceTask
            {
                Id = i,
                Overview = $"Maintenance Task {i}",
                Difficulty = (i % 5) + 1,
                Deadline = DateOnly.FromDateTime(DateTime.Now.AddDays(14)),
                ServicesToMaintain = $"Services for task {i}",
                ServersToMaintain = $"Servers for task {i}",
            });
        }

    }

    public List<TaskBase> GetAllTasks() => _tasks;
    public List<Employee> GetAllEmployees() => _employees;
}
