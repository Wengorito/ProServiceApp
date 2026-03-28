namespace ProService.API.Models.Tasks;

public class MaintenanceTask : TaskBase
{
    public DateOnly Deadline { get; set; }
    public string Services { get; set; } = string.Empty;
    public string Servers { get; set; } = string.Empty;
}
