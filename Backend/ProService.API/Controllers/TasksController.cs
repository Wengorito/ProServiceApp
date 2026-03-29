using Microsoft.AspNetCore.Mvc;
using ProService.API.DTOs;
using ProService.API.Models.Tasks;
using ProService.API.Services.Interfaces;

namespace ProService.API.Controllers;

public class TasksController(ITaskService taskService) : BaseApiController
{
    private readonly ITaskService _taskService = taskService;

    [HttpGet("available")]
    public async Task<ActionResult<IReadOnlyList<TaskBase>>> GetAvailableTasks(int page = 1)
    {
        return await _taskService.GetAvailableTasksAsync(page, pageSize: 10);
    }

    [HttpGet("assigned")]
    public async Task<ActionResult<IReadOnlyList<TaskBase>>> GetAssignedTasks(int employeeId, int page = 1)
    {
        return await _taskService.GetAssignedTasksAsync(employeeId, page, pageSize: 10);
    }

    [HttpPost("assign")]
    public async Task<ActionResult> AssignTasks([FromBody] AssignTasksDto dto)
    {
        // perform request validation
        await _taskService.AssignTasks(dto.TasksIds, dto.EmployeeId);
        return Ok();
    }
}