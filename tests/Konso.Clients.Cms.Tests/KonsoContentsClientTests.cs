using FluentAssertions;
using Konso.Clients.Cms.Domain.Contents;
using Konso.Clients.Cms.Domain.Enums;
using Konso.Clients.Cms.Domain.Interfaces;
using Konso.Clients.Cms.Domain.Sites;
using Konso.Clients.Cms.Infrastructure.Clients;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Konso.Clients.Cms.Tests
{
    public class KonsoContentsClientTests
    {
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly Mock<IHttpClientFactory> _httpClientFactoryMock;
        private readonly IKonsoContentsClient _konsoContentsClient;
        private readonly KonsoCmsSite _konsoCmsSite;
        public KonsoContentsClientTests()
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
            services.AddSingleton<IKonsoContentsClient, KonsoContentsClient>();

            var provider = services.BuildServiceProvider();
            _konsoContentsClient = provider.GetRequiredService<IKonsoContentsClient>();

            _konsoCmsSite = new KonsoCmsSite() { ApiKey = "D2tTiq2yAIGEfOidavSo+cbvlSJbMmkL3Nr62VRKuIY=", BucketId = "4583d602" };

        }

        [Fact]
        public async void simple_get()
        {

            // act
            var content = await _konsoContentsClient.GetByBucketIdAsync(new KonsoContentFilter() { SiteConfig = new KonsoCmsSite() { ApiKey = "qEU9cJ+s7Gnexhg75NyJXOo2P+FLKVUm5KZYG3yz/tM=", BucketId = "8bed044f" }, 
                From = 0, To=2, Type = 53, Sort=2, IsPublished=true });


            //
            content.Should().NotBeNull();
        }


        [Fact]
        public async void simple_post()
        {
            // arrange
            var createRequest = new CreateContentRequest()
            {
                Name = "test",
                Announce = "Let's test it",
                Body = "legacyItem.Body",
                CategoriesIds = new List<int>() { 514 },
                Type = ContentTypes.Blog,
                IsActive = true,
                Publish = true,
                Title = "Why not to test",
                Slug = "why-not-to-test",

            };

            // act
            var createResult = await _konsoContentsClient.CreateAsync(createRequest, _konsoCmsSite);
        


            // assert
            createResult.Succeeded.Should().BeTrue();
        }

    }
}
