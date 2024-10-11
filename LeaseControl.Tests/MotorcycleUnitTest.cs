using LeaseControl.Application.IServices;
using LeaseControl.Application.Services;
using LeaseControl.Domain;
using LeaseControl.Domain.InterfaceRepository;
using LeaseControl.Infrastructure.Mensageria;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RabbitMQ.Client;

namespace LeaseControl.Tests
{
    public class MotorcycleUnitTest
    {

        private readonly IMotorcycleService _motorcycleService;
        private readonly Mock<IMotorcycleRepository> _motoRepositoryMock;
        private readonly Mock<ILeaserRepository> _leaserRepositoryMock;
        private readonly Mock<IModel> _eventPublisherMock;

        public MotorcycleUnitTest()
        {
            _motoRepositoryMock = new Mock<IMotorcycleRepository>();
            _leaserRepositoryMock = new Mock<ILeaserRepository>();
            _eventPublisherMock = new Mock<IModel>();
            _motorcycleService = new MotorcycleService(_motoRepositoryMock.Object, _leaserRepositoryMock.Object, _eventPublisherMock.Object);
        }

        [Fact]
        public async Task CadastrarMoto_CadastraComSucesso()
        {
            // Arrange
            var mockRepo = new Mock<IMotorcycleRepository>();
            var mockNotifier = new Mock<MotorcycleNotifier>();
            
            var moto = new Motorcycle(2024, "Modelo X", "ABC-1234");

            mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(moto);

            // Act
            var result = await _motorcycleService.AddMotorcycle(moto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(moto, createdResult.Value);
            mockNotifier.Verify(n => n.NotifyMotorcyle(moto), Times.Once);
        }

    }
}
