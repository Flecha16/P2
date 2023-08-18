using Microsoft.AspNetCore.Routing;
using NuGet.ContentModel;
using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AuthSystem.Tests
{
    [TestFixture]
    public class Tests
    {
        private HttpClient _client;

        [SetUp]
        public void Setup()
        {
            // Configurar el cliente HTTP para realizar las solicitudes al API
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://localhost:5000"); // Reemplaza con la URL base de tu API
        }

        [Test]
        public async Task GetAllStatistics_ReturnsOK()
        {
            // Realizar una solicitud GET al endpoint correspondiente
            var response = await _client.GetAsync("/api/statistics");

            // Verificar que la respuesta sea exitosa
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            // Verificar que la respuesta sea un objeto JSON
            Assert.AreEqual("application/json", response.Content.Headers.ContentType.MediaType);

            // Leer el contenido de la respuesta como una cadena JSON
            var content = await response.Content.ReadAsStringAsync();

            // Realizar más verificaciones en base al contenido recibido
            // ...

        }

        [Test]
        public async Task GetStatisticById_ReturnsOK()
        {
            // Realizar una solicitud GET al endpoint correspondiente
            var response = await _client.GetAsync("/api/statistics/1"); // Reemplaza con el ID de una estadística existente

            // Verificar que la respuesta sea exitosa
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            // Verificar que la respuesta sea un objeto JSON
            Assert.AreEqual("application/json", response.Content.Headers.ContentType.MediaType);

            // Leer el contenido de la respuesta como una cadena JSON
            var content = await response.Content.ReadAsStringAsync();

            // Realizar más verificaciones en base al contenido recibido
            // ...

        }
    }
}
