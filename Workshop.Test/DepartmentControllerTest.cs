using System.Collections.Generic;
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

            var result = await _departmentController.GetAllDepartment();

            _mockRepo.Verify(_ => _.GetAll(), Times.Once);

            Assert.IsType<OkObjectResult>(result);
        }

    }
}