using ProService.API.Models.Employees;
using ProService.API.Models.Enums;
using ProService.API.Models.Tasks;

namespace ProService.API.Repository;

public class MockRepository : IMockRepository
{
    private List<Employee> _employees;
    private List<TaskBase> _tasks;

    public MockRepository()
    {
        _employees = new List<Employee>
        {
            new() { Id = 1, Name = "Krzysztof", Role = EmployeeRole.Developer },
            new() { Id = 2, Name = "Zenek", Role = EmployeeRole.Developer },
            new() { Id = 3, Name = "Hania", Role = EmployeeRole.DevOpsAdmin },
            new() { Id = 4, Name = "Marcin", Role = EmployeeRole.DevOpsAdmin },
            new() { Id = 5, Name = "Ewelina", Role = EmployeeRole.DevOpsAdmin }
        };

        _tasks = new List<TaskBase>();

        for (int i = 1; i <= 10; i++)
        {
            _tasks.Add(new ImplementationTask
            {
                Id = i,
                Overview = $"Implementation Task {i}",
                Difficulty = ((i - 1) % 5) + 1,
                ImplementationDetails = $"Details for task {i}"
            });
        }

        for (int i = 11; i <= 20; i++)
        {
            _tasks.Add(new DeploymentTask
            {
                Id = i,
                Overview = $"Deployment Task {i}",
                Difficulty = ((i - 1) % 5) + 1,
                Deadline = DateOnly.FromDateTime(DateTime.Now.AddDays(7)),
                DeploymentScope = $"Scope {i}",
            });
        }

        for (int i = 21; i <= 28; i++)
        {
            _tasks.Add(new MaintenanceTask
            {
                Id = i,
                Overview = $"Maintenance Task {i}",
                Difficulty = ((i - 1) % 5) + 1,
                Deadline = DateOnly.FromDateTime(DateTime.Now.AddDays(14)),
                ServicesToMaintain = $"Services for task {i}",
                ServersToMaintain = $"Servers for task {i}",
            });
        }
    }

    public async Task<IEnumerable<Employee>> GeEmployeesAsync() 
        => _employees;

    public async Task<Employee?> GetEmployeeAsync(int id)
        => _employees.FirstOrDefault(e => e.Id == id);

    public async Task<IEnumerable<TaskBase>> GetTasksAsync(IEnumerable<int> tasksIds)
        => _tasks.Where(t => tasksIds.Contains(t.Id));

    public async Task<IEnumerable<TaskBase>> GetEmployeeTasksAsync(int employeeId)
        => _tasks.Where(t => t.AssigneeId == employeeId);

    public async Task<IEnumerable<TaskBase>> GetAvailableTasksAsync(int page, int pageSize)
        => _tasks
            .Where(t => t.AssigneeId == null)
            .OrderByDescending(t => t.Difficulty)
            .Skip((page - 1) * pageSize)
            .Take(pageSize);

    public async Task<IEnumerable<TaskBase>> GetAssignedTasksAsync(int employeeId, int page, int pageSize)
        => _tasks
            .Where(t => t.AssigneeId == employeeId)
            .OrderByDescending(t => t.Difficulty)
            .Skip((page - 1) * pageSize)
            .Take(pageSize);

    public async Task AssignTasks(IEnumerable<int> tasksIds, int employeeId)
    {
        foreach(var task in _tasks.Where(t => tasksIds.Contains(t.Id)))
        {
            task.AssigneeId = employeeId;
        }

        return;
    }
}
