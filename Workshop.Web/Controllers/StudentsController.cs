using Microsoft.AspNetCore.Mvc;
using Workshop.Web.Models;
using Workshop.Web.Repository;

namespace Workshop.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentsController : ControllerBase
{
    IRepository<Student> _repository;
    public StudentsController(IRepository<Student> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IEnumerable<Student>> Get()
    {
        return await _repository.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetStudent(int id)
    {
        var student = await _repository.GetById(id);

        if (student == null)
        {
            return NotFound();
        }

        return Ok(student);
    }

    [HttpPost]
    public async Task<IActionResult> Post(Student student)
    {
        await _repository.Create(student);

        return CreatedAtAction("GetStudent", new { id = student.Id }, student);
    }
}
