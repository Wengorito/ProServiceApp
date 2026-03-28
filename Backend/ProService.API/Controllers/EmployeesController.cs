using System;
using Microsoft.AspNetCore.Mvc;
using ProService.API.Models.Employees;
using ProService.API.Repository;

namespace ProService.API.Controllers;

public class EmployeesController(IMockRepository repository) : BaseApiController(repository)
{
    [HttpGet]
    public ActionResult<IEnumerable<Employee>> GetEmployees()
    {
        return Ok(_repository.GetAllEmployees());
    }
}
