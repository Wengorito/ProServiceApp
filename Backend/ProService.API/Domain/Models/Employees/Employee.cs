using System;
using ProService.API.Domain.Models.Employees.Enums;

namespace ProService.API.Domain.Models.Employees;

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public EmployeeRole Role { get; set; }
}
