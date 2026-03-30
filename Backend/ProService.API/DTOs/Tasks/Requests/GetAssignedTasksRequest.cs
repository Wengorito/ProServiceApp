using System.ComponentModel.DataAnnotations;

namespace ProService.API.DTOs.Tasks.Requests;

public class GetAssignedTasksRequest : PaginatedRequest
{
    [Range(1, int.MaxValue, ErrorMessage = "Employee id must be integer greater than 0")]
    public int EmployeeId { get; set; }
}
