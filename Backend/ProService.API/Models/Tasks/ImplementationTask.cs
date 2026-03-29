using System.ComponentModel.DataAnnotations;

namespace ProService.API.Models.Tasks;

public class ImplementationTask : TaskBase
{
    [MaxLength(400)]
    public string? ImplementationDetails { get; set; }

    public ImplementationTask()
    {
        Type = Enums.TaskType.Implementation;
    }
}