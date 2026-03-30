using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using ProService.API.Commons;
using ProService.API.DTOs.Tasks;
using ProService.API.Services.Interfaces;

namespace ProService.API.Controllers;

public class TasksController(ITaskAssignmentService taskService) : BaseApiController
{
    private readonly ITaskAssignmentService _taskService = taskService;

    [HttpGet("available")]
    public async Task<ActionResult<IEnumerable<TaskDto>>> GetAvailableTasks(int pageNumber, int pageSize)
    {
        AdjustPagination(ref pageNumber, ref pageSize);

        try
        {
            var tasks = await _taskService.GetAvailableTasksAsync(pageNumber, pageSize);
            return Ok(tasks);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("assigned")]
    public async Task<ActionResult<IEnumerable<TaskDto>>> GetAssignedTasks(int employeeId, int pageNumber, int pageSize)
    {
        AdjustPagination(ref pageNumber, ref pageSize);

        try
        {
            var tasks = await _taskService.GetAssignedTasksAsync(employeeId, pageNumber, pageSize);
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

    private static void AdjustPagination(ref int pageNumber, ref int pageSize)
    {
        if (pageNumber < 1)
        {
            pageNumber = 1;
        }

        if (pageSize < 1 || pageSize > ApiConstants.MAX_TASK_PAGE_SIZE)
        {
            pageSize = ApiConstants.MAX_TASK_PAGE_SIZE;
        }
    }
}