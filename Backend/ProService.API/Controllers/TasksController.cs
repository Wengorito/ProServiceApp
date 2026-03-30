using Microsoft.AspNetCore.Mvc;
using ProService.API.DTOs.Tasks.Requests;
using ProService.API.DTOs.Tasks.Responses;
using ProService.API.Services.Interfaces;

namespace ProService.API.Controllers;

public class TasksController(ITaskAssignmentService taskService) : BaseApiController
{
    private readonly ITaskAssignmentService _taskService = taskService;

    [HttpPost("available")]
    public async Task<ActionResult<IEnumerable<TaskDto>>> GetAvailableTasks([FromBody] GetAvailableTasksRequest request)
    {
        // NormalizePagination(ref request.PageNumber, ref pageSize);

        try
        {
            var tasks = await _taskService.GetAvailableTasksAsync(request.PageNumber, request.PageSize);
            return Ok(tasks);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("assigned")]
    public async Task<ActionResult<IEnumerable<TaskDto>>> GetAssignedTasks(GetAssignedTasksRequest request)
    {
        try
        {
            var tasks = await _taskService.GetAssignedTasksAsync(request.EmployeeId, request.PageNumber, request.PageSize);
            return Ok(tasks);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("assign")]
    public async Task<ActionResult> AssignTasks(AssignTasksRequest request)
    {
        try
        {
            await _taskService.AssignTasks(request.TasksIds, request.EmployeeId);
            return Ok();
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}