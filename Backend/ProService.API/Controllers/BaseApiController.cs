using Microsoft.AspNetCore.Mvc;
using ProService.API.Repository;

namespace ProService.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public abstract class BaseApiController : ControllerBase
{
    protected readonly IMockRepository _repository;

    protected BaseApiController(IMockRepository repository)
    {
        _repository = repository;
    }
}