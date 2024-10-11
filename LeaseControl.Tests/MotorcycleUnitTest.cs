using LeaseControl.Application.IServices;
using LeaseControl.Application.Services;
using LeaseControl.Domain;
using LeaseControl.Domain.InterfaceRepository;
using LeaseControl.Infrastructure.Mensageria;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RabbitMQ.Client;
using System.Security.Cryptography.Xml;

namespace LeaseControl.Tests
{
    public class MotorcycleTest
    {

        private readonly IMotorcycleService _motorcycleService;
        private readonly Mock<IMotorcycleRepository> _motoRepositoryMock;
        private readonly Mock<ILeaserRepository> _leaserRepositoryMock;
        private readonly Mock<IModel> _eventPublisherMock;

        public MotorcycleTest()
        {
            _motoRepositoryMock = new Mock<IMotorcycleRepository>();
            _leaserRepositoryMock = new Mock<ILeaserRepository>();
            _eventPublisherMock = new Mock<IModel>();
            _motorcycleService = new MotorcycleService(_motoRepositoryMock.Object, _leaserRepositoryMock.Object, _eventPublisherMock.Object);
        }

        [Fact]
        public async Task AddMotorcycle_Sucess()
        {
            // Arrange
            var mockRepo = new Mock<IMotorcycleRepository>();
            var mockNotifier = new Mock<MotorcycleNotifier>();

            var moto = new Motorcycle()
            {
                Id = new Guid(),
                Model = "Modelo X",
                Year = 2024,
                Plate = "ABC-1234",
                Leases = new List<Lease>()
            };

            mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(moto);

            // Act
            var result = await _motorcycleService.AddMotorcycle(moto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(moto, createdResult.Value);
            mockNotifier.Verify(n => n.NotifyMotorcyle(moto), Times.Once);
        }


        [Fact]
        public void AddMotorcycle_ExistsPlate_Exception()
        {
            var moto = new Motorcycle()
            {
                Id = new Guid(),
                Model = "Modelo X",
                Year = 2024,
                Plate = "ABC-1234",
                Leases = new List<Lease>()
            };

            var moto2 = new Motorcycle()
            {
                Id = new Guid(),
                Model = "Modelo X",
                Year = 2024,
                Plate = "ABC-1234",
                Leases = new List<Lease>()
            };
            // Arrange
            _motoRepositoryMock.Setup(repo => repo.GetByPlateAsync(It.IsAny<string>())).ReturnsAsync(moto);

            // Act & Assert
            Assert.ThrowsAsync<Exception>(() => _motorcycleService.AddMotorcycle(moto2));
        }

        [Fact]
        public void GetMotorcycle_WhenPlateExists_ReturnNull()
        {
            // Arrange
            string plate = "XYZ-9999";
            Motorcycle  motorcycle = new ();
            _motoRepositoryMock.Setup(repo => repo.GetByPlateAsync(It.IsAny<string>())).ReturnsAsync((motorcycle));

            // Act
            Guid guid = Guid.Parse(plate); 
            var result = _motorcycleService.GetByIdMotorcycle(guid);

            // Assert
            Assert.Null(result.Result);
        }

        [Fact]
        public void UpdatePlate()
        {
            // Arrange
            var moto = new Motorcycle()
            {
                Id = new Guid(),
                Model = "Modelo X",
                Year = 2024,
                Plate = "ABC-1234",
                Leases = new List<Lease>()
            };

            _motoRepositoryMock.Setup(repo => repo.GetByPlateAsync(It.IsAny<string>())).ReturnsAsync(moto);
            _motoRepositoryMock.Setup(repo => repo.UpdateAsync(moto)).Verifiable();

            // Act
            _motorcycleService.UpdateMotorcycle(moto.Id, "DEF-5678");

            // Assert
            Assert.Equal("DEF-5678", moto.Plate);
            _motoRepositoryMock.Verify(repo => repo.UpdateAsync(moto), Times.Once);
        }
    }
}
