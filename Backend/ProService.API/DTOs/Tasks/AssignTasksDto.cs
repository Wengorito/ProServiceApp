namespace ProService.API.DTOs.Tasks;

public record AssignTasksDto (List<int> TasksIds, int EmployeeId);
