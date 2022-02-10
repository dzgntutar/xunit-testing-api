using Microsoft.AspNetCore.Mvc;
using Workshop.Web.Models;
using Workshop.Web.Repository;

namespace Workshop.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class DepartmentsController : ControllerBase
{

    private IRepository<Department> _repository;

    public DepartmentsController(IRepository<Department> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var departments = await _repository.GetAll();
        return Ok(departments);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var department = await _repository.GetById(id);

        if (department is null)
        {
            return NotFound();
        }

        return Ok(department);
    }
}