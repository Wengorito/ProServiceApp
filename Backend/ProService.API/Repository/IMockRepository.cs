using ProService.API.Models.Employees;
using ProService.API.Models.Tasks;

namespace ProService.API.Repository;

public interface IMockRepository
{
    List<Employee> GetAllEmployees();
    List<TaskBase> GetAllTasks();
}
