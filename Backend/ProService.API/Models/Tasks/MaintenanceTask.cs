using System.ComponentModel.DataAnnotations;

namespace ProService.API.Models.Tasks;

public class MaintenanceTask : TaskBase
{
    public DateOnly? Deadline { get; set; }

    [MaxLength(400)]
    public string? ServicesToMaintain { get; set; }

    [MaxLength(400)]
    public string? ServersToMaintain { get; set; }

    public MaintenanceTask()
    {
        TaskType = Enums.TaskType.Maintenance;
    }
}
