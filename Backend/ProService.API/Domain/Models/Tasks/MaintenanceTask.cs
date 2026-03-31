using System.ComponentModel.DataAnnotations;

namespace ProService.API.Domain.Models.Tasks;

public class MaintenanceTask : TaskBase
{
    public DateOnly? Deadline { get; set; }

    [MaxLength(400)]
    public string? ServicesToMaintain { get; set; }

    [MaxLength(400)]
    public string? ServersToMaintain { get; set; }

    public MaintenanceTask()
    {
        Type = Enums.TaskType.Maintenance;
    }
}
