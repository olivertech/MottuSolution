namespace Mottu.UnitTests.Services
{
    public class UserTypeTests : IDisposable
    {
        protected readonly AppDbContext _context;

        public UserTypeTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                          .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                          .Options;
            _context = new AppDbContext(options);
            _context.Database.EnsureCreated();
        }

        [Fact(DisplayName = "Teste GetAll - Service")]
        public void GetAll_ReturnUserTypeCollection()
        {
            // Arrange
            var unitofWork = new Mock<IUnitOfWork>();
            var mapper = new Mock<IMapper>();
            unitofWork.Setup(_ => _.userTypeRepository.GetAll().Result).Returns(UserTypeMockData.GetUserTypeList());

            if (_context.UserTypes.Count() == 0)
            {
                _context.UserTypes.AddRange(UserTypeMockData.GetUserTypeList());
                _context.SaveChanges();
            }

            // Act
            var sut = new UserTypeService(unitofWork.Object, mapper.Object);
            var result = sut.GetAll();

            // Assert
            Assert.Equal(3, result.Result.Content!.Count());
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
