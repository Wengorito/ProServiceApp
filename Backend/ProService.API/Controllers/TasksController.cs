using Microsoft.AspNetCore.Mvc;
using ProService.API.Models.Tasks;
using ProService.API.Services.Interfaces;

namespace ProService.API.Controllers;

public class TasksController(ITaskService taskService) : ControllerBase
{
    private readonly ITaskService _taskService = taskService;

    [HttpGet("available")]
    public async Task<ActionResult<IReadOnlyList<TaskBase>>> GetAvailableTasks(int page = 1)
    {
        return await _taskService.GetAvailableTasksAsync(page, pageSize: 10);
    }

    [HttpGet("assigned")]
    public async Task<ActionResult<IReadOnlyList<TaskBase>>> GetEmployeeAssignedTasks(int employeeId, int page = 1)
    {
        return await _taskService.GetEmployeeAssignedTasksAsync(employeeId, page, pageSize: 10);
    }

    [HttpPost("assign")]
    public async Task<ActionResult> AssignTasks([FromBody] IEnumerable<int> taskIds, int employeeId)
    {
        await _taskService.AssignTasks(taskIds, employeeId);
        return Ok();
    }
}