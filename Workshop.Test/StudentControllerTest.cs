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
                new Student {Id=1, Name = "Özgür", Surname= "Murat",  Department = "1"    }
            };
        }

        [Fact]
        public async void GetStudents_ExecuteAction_ReturnOkWithStudens()
        {
            _mockRepository.Setup(x => x.GetAll()).ReturnsAsync(students);

            var result = await _controller.GetStudents();

            var ok = Assert.IsType<OkObjectResult>(result);

            var returnStudens = Assert.IsAssignableFrom<IEnumerable<Student>>(ok.Value);

            Assert.Equal(returnStudens.ToList().Count, students.Count);
        }
    }
}
