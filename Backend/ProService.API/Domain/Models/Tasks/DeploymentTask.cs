using System.ComponentModel.DataAnnotations;

namespace ProService.API.Domain.Models.Tasks;

public class DeploymentTask : TaskBase
{
    public DateOnly? Deadline { get; set; }

    [MaxLength(400)]
    public string? DeploymentScope { get; set; }

    public DeploymentTask()
    {
        Type = Enums.TaskType.Deployment;
    }
}