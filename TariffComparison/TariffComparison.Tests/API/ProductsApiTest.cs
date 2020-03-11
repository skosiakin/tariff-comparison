using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TariffComparison.API;
using TariffComparison.Service.Models;
using Xunit;

namespace TariffComparison.Tests.API
{
    public class ProductsApiTest
    {
        private readonly HttpClient _client;

        public ProductsApiTest()
        {
            var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = server.CreateClient();
        }

        [Fact]
        public async Task Products_should_return_http_code_400_when_consumption_is_negative()
        {
            // Arrange
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/products/-100");

            // Act
            var response = await _client.SendAsync(request);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Products_should_return_sorted_product_prices()
        {
            // Arrange
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/products/4500");

            // Act
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = JsonConvert.DeserializeObject<List<ProductCost>>(content);
            result.Count.Should().Be(2);
            result[0].Cost.Should().Be(950);
            result[1].Cost.Should().Be(1050);
        }
    }
}
