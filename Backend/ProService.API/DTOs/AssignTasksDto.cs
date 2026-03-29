namespace ProService.API.DTOs;

public class AssignTasksDto
{
    public required List<int> TasksIds { get; set; }
    public required int EmployeeId { get; set; }
}
