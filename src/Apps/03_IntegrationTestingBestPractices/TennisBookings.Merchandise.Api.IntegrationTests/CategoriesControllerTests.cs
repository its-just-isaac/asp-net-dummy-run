using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace TennisBookings.Merchandise.Api.IntegrationTests
{
    public class CategoriesControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _httpClient;

        public CategoriesControllerTests(WebApplicationFactory<Startup> factory)
        {
            // option 1
            // _httpClient = factory.CreateDefaultClient(new Uri("http://localhost/api/Categories"));

            // option 2
            factory.ClientOptions.BaseAddress = new Uri("http://localhost/api/Categories");
            _httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task GetAll_ReturnSuccessStatusCode()
        {
            var response = await _httpClient.GetAsync("");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("application/json", response.Content.Headers.ContentType.MediaType);
        }
    }
}