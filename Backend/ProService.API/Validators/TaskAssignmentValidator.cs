using ProService.API.Models.Employees;
using ProService.API.Models.Enums;
using ProService.API.Models.Tasks;
using ProService.API.Models.Tasks.Enums;

namespace ProService.API.Validators;

public class TaskAssignmentValidator : ITaskAssignmentValidator
{
    public void Validate(Employee employee, IReadOnlyList<TaskBase> currentTasks, IReadOnlyList<TaskBase> newTasks)
    {
        ValidateAlreadyAssigned(newTasks);
        ValidateEmployeeRole(employee, newTasks);

        var allTasks = currentTasks.Concat(newTasks).ToList();

        ValidateTaskCount(allTasks);
        ValidateDifficulty(allTasks);
    }

    private void ValidateAlreadyAssigned(IReadOnlyList<TaskBase> tasks)
    {
        if (tasks.Any(t => t.AssigneeId != null))
        {
            throw new Exception("One or more tasks are already assigned");
        }
    }

    private void ValidateEmployeeRole(Employee employee, IReadOnlyList<TaskBase> tasks)
    {
        if(employee.Role == EmployeeRole.Developer &&
            tasks.Any(t => t.Type != TaskType.Implementation))
        {
            throw new Exception("Developer can only have implementation tasks");
        }
    }

    private void ValidateTaskCount(IReadOnlyList<TaskBase> tasks)
    {
        if (tasks.Count < 5 || tasks.Count > 11)
        {
            throw new Exception("Employee must have between 5 and 11 tasks assigned");
        }
    }

    private void ValidateDifficulty(List<TaskBase> tasks)
    {
        var hard = tasks.Count(t => t.Difficulty >= 4);
        var hardRatio = (double)hard / tasks.Count * 100;

        if (hardRatio < 10 || hardRatio > 30)
            throw new Exception("Hard tasks must be between 10% and 30%");

        var easy = tasks.Count(t => t.Difficulty <= 2);
        var easyRatio = (double)easy / tasks.Count * 100;

        if (easyRatio > 50)
        {
            throw new Exception("Easy tasks cannot exceed 50%");
        }
    }    
}