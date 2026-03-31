using ProService.API.Domain.Models.Employees;
using ProService.API.Domain.Models.Tasks;

namespace ProService.API.Application.Validators;

public interface ITaskAssignmentValidator
{
    void Validate(Employee employee, IReadOnlyList<TaskBase> currentTasks, IReadOnlyList<TaskBase> newTasks);
}