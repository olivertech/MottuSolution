using AutoMapper;
using FluentAssertions;
using Moq;
using Mottu.Application.Services;
using Mottu.Domain.Entities;
using Mottu.Domain.Interfaces;
using RestSharp;
using System.Net;

namespace Mottu.Tests
{
    [TestFixture]
    public class UserTypeTests
    {
        private Mock<IUnitOfWork> _mockUnitofWork = new Mock<IUnitOfWork>();
        private Mock<IMapper> _mockMapper = new Mock<IMapper>();

        [Test]
        public void Get_UserTypeService_ReturnsStatusCodeOk()
        {
            var service = new UserTypeService(_mockUnitofWork.Object, _mockMapper.Object);
            var result = service.GetAll();

            // Assert
            Assert.That(result.Result, Has.Exactly(3).Items);
        }
    }
}