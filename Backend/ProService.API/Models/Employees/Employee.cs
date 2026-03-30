using System;
using ProService.API.Models.Enums;

namespace ProService.API.Models.Employees;

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public EmployeeRole Role { get; set; }
}
