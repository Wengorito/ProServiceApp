using System.ComponentModel.DataAnnotations;

namespace ProService.API.DTOs.Tasks.Requests;

public record AssignTasksRequest
{
    [Range(1, int.MaxValue, ErrorMessage = "EmployeeId must be integer greater than 0.")]
    public int EmployeeId { get; init; }
    
    [Required(ErrorMessage = "TaskIds list is required.")]
    [MinLength(1, ErrorMessage = "At least one task must be provided.")]
    [UniqueItems()]
    public required List<int> TasksIds { get; init; }
}

public class UniqueItemsAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not IEnumerable<int> list)
            return new ValidationResult("Invalid list type.");

        var distinctCount = list.Distinct().Count();
        var totalCount = list.Count();

        if (distinctCount != totalCount)
            return new ValidationResult(ErrorMessage ?? "List contains duplicates.");

        if(list.Any(x => x < 1 || x > int.MaxValue))
            return new ValidationResult(ErrorMessage ?? "Task IDs must be integers greater than 0.");

        return ValidationResult.Success;
    }
}
