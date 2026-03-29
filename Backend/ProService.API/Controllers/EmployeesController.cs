using Microsoft.AspNetCore.Mvc;
using ProService.API.Models.Employees;
using ProService.API.Services.Interfaces;

namespace ProService.API.Controllers;

public class EmployeesController(IEmployeeService employeeService) : BaseApiController
{
    private readonly IEmployeeService _employeeService = employeeService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
    {
        return Ok(await _employeeService.GetAllEmployeesAsync());
    }
}
