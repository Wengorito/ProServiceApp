namespace ProService.API.Models.Tasks;

public class DeploymentTask : TaskBase
{
    public DateOnly Deadline { get; set; }
    public string Scope { get; set; } = string.Empty;
}