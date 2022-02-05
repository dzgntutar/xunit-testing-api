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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetStudentById(int id)
    {
        var student = await _repository.GetById(id);

        if (student == null)
        {
            return NotFound();
        }

        return Ok(student);
    }

    [HttpPost]
    public async Task<IActionResult> SaveStudent(Student student)
    {
        await _repository.Create(student);

        return CreatedAtAction("GetStudentById", new { id = student.Id }, student);
    }

    [HttpPut]
    public IActionResult UpdateStudent(int studentId, Student student)
    {
        if (studentId != student.Id)
            return BadRequest();

        _repository.Update(student);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent(int studentId)
    {
        var student = await _repository.GetById(studentId);

        if (student == null)
        {
            return NotFound();
        }

        _repository.Delete(student);

        return NoContent();
    }
}
