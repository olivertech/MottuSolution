namespace Mottu.UnitTests.Controllers
{
    public class UserTypeTests
    {
        [Fact(DisplayName = "Teste GetAll - Controller")]
        public void GetAll_ReturnsStatus200()
        {
            // Arrange
            var userTypeService = new Mock<IUserTypeService>();
            var unitofWork = new Mock<IUnitOfWork>();
            var mapper = new Mock<IMapper>();
            userTypeService.Setup(_ => _.GetAll().Result).Returns(UserTypeMockData.GetUserTypes());

            // Act
            var sut = new UserTypeController(unitofWork.Object, mapper.Object, userTypeService.Object);
            var result = (OkObjectResult)sut.GetAll();

            // Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact(DisplayName = "Teste GetById - Controller")] 
        public void GetById_ReturnsStatus200()
        {
            // Arrange
            var userTypeService = new Mock<IUserTypeService>();
            var unitofWork = new Mock<IUnitOfWork>();
            var mapper = new Mock<IMapper>();
            userTypeService.Setup(_ => _.GetById(Guid.Parse("f6a2372a-b146-45f9-be70-a0be13736dd8")).Result).Returns(UserTypeMockData.GetUserType(Guid.Parse("f6a2372a-b146-45f9-be70-a0be13736dd8")));

            // Act
            var sut = new UserTypeController(unitofWork.Object, mapper.Object, userTypeService.Object);
            var result = (OkObjectResult)sut.GetById(Guid.Parse("f6a2372a-b146-45f9-be70-a0be13736dd8"));

            // Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact(DisplayName = "Teste GetUserTypeByName/ADMINISTRADOR - Controller")]
        public void GetByName_ADMINISTRADOR_ReturnsStatus200()
        {
            // Arrange
            var userTypeService = new Mock<IUserTypeService>();
            var unitofWork = new Mock<IUnitOfWork>();
            var mapper = new Mock<IMapper>();
            userTypeService.Setup(_ => _.GetListByName("ADMINISTRADOR").Result).Returns(UserTypeMockData.GetUserTypeByName("ADMINISTRADOR"));

            // Act
            var sut = new UserTypeController(unitofWork.Object, mapper.Object, userTypeService.Object);
            var result = (OkObjectResult)sut.GetListByName("ADMINISTRADOR");

            // Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact(DisplayName = "Teste GetUserTypeByName/ENTREGADOR - Controller")]
        public void GetByName_ENTREGADOR_ReturnsStatus200()
        {
            // Arrange
            var userTypeService = new Mock<IUserTypeService>();
            var unitofWork = new Mock<IUnitOfWork>();
            var mapper = new Mock<IMapper>();
            userTypeService.Setup(_ => _.GetListByName("ENTREGADOR").Result).Returns(UserTypeMockData.GetUserTypeByName("ENTREGADOR"));

            // Act
            var sut = new UserTypeController(unitofWork.Object, mapper.Object, userTypeService.Object);
            var result = (OkObjectResult)sut.GetListByName("ENTREGADOR");

            // Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact(DisplayName = "Teste GetUserTypeByName/CONSUMIDOR - Controller")]
        public void GetByName_CONSUMIDOR_ReturnsStatus200()
        {
            // Arrange
            var userTypeService = new Mock<IUserTypeService>();
            var unitofWork = new Mock<IUnitOfWork>();
            var mapper = new Mock<IMapper>();
            userTypeService.Setup(_ => _.GetListByName("CONSUMIDOR").Result).Returns(UserTypeMockData.GetUserTypeByName("CONSUMIDOR"));

            // Act
            var sut = new UserTypeController(unitofWork.Object, mapper.Object, userTypeService.Object);
            var result = (OkObjectResult)sut.GetListByName("CONSUMIDOR");

            // Assert
            result.StatusCode.Should().Be(200);
        }
    }
}