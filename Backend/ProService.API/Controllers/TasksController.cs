using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProService.API.Models.Tasks;
using ProService.API.Repository;

namespace ProService.API.Controllers;

public class TasksController(IMockRepository repository) : BaseApiController(repository)
{
    [HttpGet("available")]
    public ActionResult<IEnumerable<TaskBase>> GetAvailableTasks(int page = 1)
    {
        var availableTasks = _repository
                .GetAllTasks()
                .Where(t => t.AssignedEmployeeId == null)
                .Skip((page - 1) * 10)
                .Take(10)
                .ToList();

        return Ok(availableTasks);
    }
}

