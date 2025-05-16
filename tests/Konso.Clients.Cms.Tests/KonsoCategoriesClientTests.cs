using FluentAssertions;
using GetInfra.WebApi.Abstractions.Models.Responses;
using Konso.Clients.Cms.Domain.Contents;
using Konso.Clients.Cms.Domain.Enums;
using Konso.Clients.Cms.Domain.Interfaces;
using Konso.Clients.Cms.Domain.Sites;
using Konso.Clients.Cms.Infrastructure.Clients;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Net;

namespace Konso.Clients.Cms.Tests
{
    public class KonsoCategoriesClientTests
    {
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly Mock<IHttpClientFactory> _httpClientFactoryMock;
        private readonly IKonsoCategoriesClient _konsoCategoriesClient;
        private readonly KonsoCmsSite _konsoCmsSite;
        public KonsoCategoriesClientTests()
        {
            var services = new ServiceCollection();
            //var httpClient = CreateMockHttpClient();

            // Add Configuration
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                { "Konso:Cms:Endpoint", "https://apis-spb.konso.io" } // Mock Configuration
                })
                .Build();

            services.AddSingleton<IConfiguration>(configuration);

            services.AddHttpClient();
            services.AddSingleton<IKonsoCategoriesClient, KonsoCategoriesClient>();

            var provider = services.BuildServiceProvider();
            _konsoCategoriesClient = provider.GetRequiredService<IKonsoCategoriesClient>();

            _konsoCmsSite = new KonsoCmsSite() { ApiKey = "D2tTiq2yAIGEfOidavSo+cbvlSJbMmkL3Nr62VRKuIY=", BucketId = "4583d602" };

        }

        [Fact]
        public async Task Get_ReturnsOk_WhenValidBucketIdProvided()
        {
            // Arrange

            // Act
            var response = await _konsoCategoriesClient.GetByBucketIdAsync(_konsoCmsSite, null, 0, 10);

            // Assert
            response.Should().NotBeNull();
            response.List.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task Get_ReturnsBadRequest_WhenBucketIdIsInvalid()
        {
            // Arrange
            _konsoCmsSite.BucketId = "1234567890";
         

            // Act
            var response = await _konsoCategoriesClient.GetByBucketIdAsync(_konsoCmsSite, 2, 0, 10);

            // Assert
            response.Should().NotBeNull();
            response.List.Should().BeNull();
        }

        [Fact]
        public async Task Get_ReturnsFilteredCategories_WhenSectionIsProvided()
        {
            // Arrange
            int section = 2;

            // Act
            var response = await _konsoCategoriesClient.GetByBucketIdAsync(_konsoCmsSite, section, 0, 10);

            // Assert
            response.Should().NotBeNull();
            response.List.Count.Should().BeGreaterThan(0);
            foreach (var category in response.List)
            {
                category.Section.Should().Be(section);
            }
        }


    }
}
