using Microsoft.AspNetCore.Mvc;
using Workshop.Web.Models;
using Workshop.Web.Repository;

namespace Workshop.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class DepartmentsController : ControllerBase
{
    private readonly IRepository<Department> _repositoryDepartment;
    private readonly IRepository<Student> _repositoryStudent;

    public DepartmentsController(IRepository<Department> repositoryDepartment, IRepository<Student> repositoryStudent)
    {
        _repositoryDepartment = repositoryDepartment;
        _repositoryStudent = repositoryStudent;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var departments = await _repositoryDepartment.GetAll();
        return Ok(departments);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var department = await _repositoryDepartment.GetById(id);

        if (department is null)
        {
            return NotFound();
        }

        return Ok(department);
    }

    [HttpPost]
    public async Task<IActionResult> Post(Department department)
    {
        await _repositoryDepartment.Create(department);

        return CreatedAtAction("Get", new { id = department.Id }, department);
    }

    [HttpGet("{id}/students")]
    public IActionResult GetStudents(string id)
    {
        var result = _repositoryStudent.GetByExpression(x => x.Department == id);

        return Ok(result);
    }
}