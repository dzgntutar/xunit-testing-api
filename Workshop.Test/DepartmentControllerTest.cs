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

        private readonly Mock<IRepository<Department>> _mockRepo;
        private readonly DepartmentsController _departmentController;

        private List<Department> _departmentList;

        public DepartmentControllerTest()
        {
            _mockRepo = new Mock<IRepository<Department>>();
            _departmentController = new DepartmentsController(_mockRepo.Object);

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
            _mockRepo.Setup(x => x.GetAll()).ReturnsAsync(_departmentList);

            var result = await _departmentController.Get();

            _mockRepo.Verify(_ => _.GetAll(), Times.Once);

            Assert.IsType<OkObjectResult>(result);
        }

        [Theory]
        [InlineData(0)]
        public async void GetById_SendInvalidId_ReturnNotFound(int id)
        {
            Department? department = null;

            _mockRepo.Setup(_ => _.GetById(id)).ReturnsAsync(department);

            var result = await _departmentController.Get(id);

            Assert.IsType<NotFoundResult>(result);
        }

    }
}