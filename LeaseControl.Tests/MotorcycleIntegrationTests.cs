using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LeaseControl.Tests
{
    public class MotorcycleIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public MotorcycleIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CadastrarMoto_ReturnsCreated()
        {
            // Arrange
            var moto = new { Ano = 2024, Modelo = "Modelo X", Placa = "ABC-1234" };

            // Act
            var response = await _client.PostAsJsonAsync("/api/motos", moto);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
    }
}
