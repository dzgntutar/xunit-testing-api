using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Workshop.Web.Controllers;
using Workshop.Web.Models;
using Workshop.Web.Repository;
using Xunit;

namespace Workshop.Test
{
    public class StudentControllerTest
    {
        private readonly Mock<IRepository<Student>> _mockRepository;
        private readonly StudentsController _controller;

        private List<Student> students;

        public StudentControllerTest()
        {
            _mockRepository = new Mock<IRepository<Student>>();
            _controller = new StudentsController(_mockRepository.Object);

            students = new List<Student>
            {
                new Student {Id=1, Name = "Kaya",  Surname= "Özgül",  Department = "1"    },
                new Student {Id=2, Name = "Özgür", Surname= "Murat",  Department = "1"    }
            };
        }

        [Fact]
        public async void GetStudents_ExecuteAction_ReturnOkWithStudens()
        {
            _mockRepository.Setup(x => x.GetAll()).ReturnsAsync(students);

            var result = await _controller.Get();

            var ok = Assert.IsType<OkObjectResult>(result);

            var returnStudents = Assert.IsAssignableFrom<IEnumerable<Student>>(ok.Value);

            Assert.Equal(returnStudents.ToList().Count, students.Count);
        }

        [Theory]
        [InlineData(0)]
        public async void GetStudentById_IdInValid_ReturnNotFound(int studentId)
        {
            Student? student = null;

            _mockRepository.Setup(x => x.GetById(studentId)).ReturnsAsync(student);

            var result = await _controller.Get(studentId);

            Assert.IsType<NotFoundResult>(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async void GetStudentById_IdSend_ReturnNotFound(int studentId)
        {
            var s = students.First(x => x.Id == studentId);

            _mockRepository.Setup(x => x.GetById(studentId)).ReturnsAsync(s);

            var result = await _controller.Get(studentId);

            var okMessage = Assert.IsType<OkObjectResult>(result);

            Student student = Assert.IsAssignableFrom<Student>(okMessage.Value);

            Assert.Equal(studentId, s.Id);
            Assert.Equal(student.Name, s.Name);
        }

        [Fact]
        public async void InserStudent_Execute_ReturnCreatedAtAction()
        {
            var student = students.First();

            _mockRepository.Setup(x => x.Create(student)).Returns(Task.CompletedTask);

            var result = await _controller.Post(student);

            var createdAtAction = Assert.IsType<CreatedAtActionResult>(result);

            _mockRepository.Verify(x => x.Create(student), Times.Once);

            Assert.Equal("GetStudentById", createdAtAction.ActionName);
        }

        [Theory]
        [InlineData(1)]
        public void UpdateStudent_IdIsNotInDb_ReturnBadRequest(int studentId)
        {
            Student student = students.First(x => x.Id == studentId);

            var result = _controller.Update(2, student);

            var returnObject = Assert.IsType<BadRequestResult>(result);

            Assert.Equal(400, returnObject.StatusCode);
        }

        [Theory]
        [InlineData(1)]
        public void UpdateStudent_ExecuteAction_ReturnNocontect(int studentId)
        {
            var student = students.First(x => x.Id == studentId);
            _mockRepository.Setup(x => x.Update(student));

            var result = _controller.Update(studentId, student);

            _mockRepository.Verify(c => c.Update(student), Times.Once);

            Assert.IsType<NoContentResult>(result);

        }

        [Theory]
        [InlineData(0)]
        public void DeleteStudent_ExecuteAction_ReturnNotFound(int studentId)
        {
            Student student = students.FirstOrDefault(x => x.Id == studentId);
            _mockRepository.Setup(x => x.Delete(student));

            var resultNotFound = _controller.Delete(studentId);

            Assert.IsType<NotFoundResult>(resultNotFound.Result);
        }

        [Theory]
        [InlineData(1)]
        public void DeleteStudent_ExecuteAction_ReturnNoContent(int studentId)
        {
            var student = students.FirstOrDefault(x => x.Id == studentId);
            _mockRepository.Setup(x => x.GetById(studentId)).ReturnsAsync(student);
            _mockRepository.Setup(x => x.Delete(student));

            var resultNoContent = _controller.Delete(studentId);

            Assert.IsType<NoContentResult>(resultNoContent.Result);
        }
    }
}
