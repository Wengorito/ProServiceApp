using ProService.API.Models.Employees;
using ProService.API.Models.Tasks;

namespace ProService.API.Validators;

public interface ITaskAssignmentValidator
{
    void Validate(Employee employee, IReadOnlyList<TaskBase> currentTasks, IReadOnlyList<TaskBase> newTasks);
}