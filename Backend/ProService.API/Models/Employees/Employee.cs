using System;
using ProService.API.Models.Enums;

namespace ProService.API.Models.Employees;

public class Employee
{
    public required int Id { get; set; }
    public required string Name { get; set; } = string.Empty;
    public required EmployeeRole Role { get; set; }
}
