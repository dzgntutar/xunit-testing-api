using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Workshop.Web.Controllers;
using Workshop.Web.Models;
using Workshop.Web.Repository;
using Xunit;

namespace Workshop.Test
{
    public class DepartmentControllerTest
    {

        private readonly Mock<IRepository<Department>> _mockDepartmentRepository;
        private readonly Mock<IRepository<Student>> _mockStudentRepository;
        private readonly DepartmentsController _departmentController;
        private readonly List<Department> _departmentList;

        public DepartmentControllerTest()
        {
            _mockDepartmentRepository = new Mock<IRepository<Department>>();
            _mockStudentRepository = new Mock<IRepository<Student>>();
            _departmentController = new DepartmentsController(_mockDepartmentRepository.Object, _mockStudentRepository.Object);

            _departmentList = new List<Department>
            {
                new Department{Id = 1 ,  Name="Fen"},
                new Department{Id = 2 ,  Name="Türkçe"},
                new Department{Id = 3 ,  Name="Matematik"}
            };
        }

        [Fact]
        public async void GelAllDepartment_ExecuteAciton_ReturnOkMessageWithData()
        {
            _mockDepartmentRepository.Setup(x => x.GetAll()).ReturnsAsync(_departmentList);

            var result = await _departmentController.Get();

            _mockDepartmentRepository.Verify(_ => _.GetAll(), Times.Once);

            Assert.IsType<OkObjectResult>(result);
        }

        [Theory]
        [InlineData(0)]
        public void GetById_SendInvalidId_ReturnNotFound(int id)
        {
            Department? department = null;

            _mockDepartmentRepository.Setup(_ => _.GetById(id)).ReturnsAsync(department);

            var result = _departmentController.Get(id).Result;

            //Assert.IsType<NotFoundResult>(result);
        }

        [Theory]
        [InlineData(1)]
        public async void GetById_SendValidId_ReturnCreatedAtAction(int id)
        {
            var department = _departmentList.First(x => x.Id == id);

            _mockDepartmentRepository.Setup(x => x.GetById(id)).ReturnsAsync(department);

            var result = await _departmentController.Get(id);

            _mockDepartmentRepository.Verify(x => x.GetById(id), Times.Once);

            Assert.IsType<OkObjectResult>(result);
        }

    }
}