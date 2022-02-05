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
    public async Task<IActionResult> GetStudents()
    {
        var students = await _repository.GetAll();
        return Ok(students);
    }

    // [HttpGet("{id}")]
    // public async Task<IActionResult> GetStudentById(int id)
    // {
    //     var student = await _repository.GetById(id);

    //     if (student == null)
    //     {
    //         return NotFound();
    //     }

    //     return Ok(student);
    // }

    // [HttpPost]
    // public async Task<IActionResult> Post(Student student)
    // {
    //     await _repository.Create(student);

    //     return CreatedAtAction("GetById", new { id = student.Id }, student);
    // }
}
