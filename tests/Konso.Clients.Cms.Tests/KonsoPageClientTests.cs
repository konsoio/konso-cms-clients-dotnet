using FluentAssertions;
using Konso.Clients.Cms.Domain.Interfaces;
using Konso.Clients.Cms.Domain.Meta;
using Konso.Clients.Cms.Domain.Pages;
using Konso.Clients.Cms.Domain.Sites;
using Konso.Clients.Cms.Infrastructure.Clients;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Konso.Clients.Cms.Tests
{
    public class KonsoPagesClientTests
    {
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly Mock<IHttpClientFactory> _httpClientFactoryMock;
        private readonly IKonsoPagesClient _konsoPagesClient;
        private readonly KonsoCmsSite _konsoCmsSite;
        public KonsoPagesClientTests()
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
            services.AddSingleton<IKonsoPagesClient, KonsoPagesClient>();

            var provider = services.BuildServiceProvider();
            _konsoPagesClient = provider.GetRequiredService<IKonsoPagesClient>();

            _konsoCmsSite = new KonsoCmsSite() { ApiKey = "D2tTiq2yAIGEfOidavSo+cbvlSJbMmkL3Nr62VRKuIY=", BucketId = "4583d602" };

        }

        [Fact]
        public async void simple_get()
        {

            // act
            var content = await _konsoPagesClient.GetByBucketIdAsync(_konsoCmsSite, 2, null, null, 0, 10);


            //
            content.Should().NotBeNull();
        }

        [Fact]
        public async void simple_get_by_slug()
        {
            var slug = "new-page-35794";
            // act
            var pages = await _konsoPagesClient.GetByBucketIdAsync(_konsoCmsSite, 2, slug, null, 0, 1);


            //
            pages.Should().NotBeNull();
            pages.Total.Should().Be(1);
            pages.List.Count.Should().Be(1);

            var page = pages.List.First();
            page.Slug.Should().Be(slug);
        }


        [Fact]
        public async void simple_get_by_slug_method()
        {
            var slug = "blog";
            // act
            var page = await _konsoPagesClient.GetBySlugAsync(_konsoCmsSite, slug);


            //
            page.Should().NotBeNull();
        
            page.Slug.Should().Be(slug);
        }


        [Fact]
        public async void simple_post()
        {
            // arrange
            var request = new CreatePageRequest
            {
                Announce = "new page announce",
                MasterPageId = 0,
                Name = "new page",
                PageType = Domain.Enums.PageTypes.ContentPage,
                Slug = $"new-page-{Random.Shared.Next(12345,99999)}",
                Title = "New page",
                Css = "",
                MetaData = new List<CreateMetaDataRequest>(),
                Contents = new List<CreatePageComponentRequest>()
            };


            request.MetaData.Add(new CreateMetaDataRequest() { Key = "description", Value = "new description" });



            request.Contents.Add(new CreatePageComponentRequest() { Body = "new txt body", Name = $"{request.Name}-component", Type = Domain.Enums.ContentTypes.InlineHTML });
       


            // act
            var createResult = await _konsoPagesClient.CreateAsync(request, _konsoCmsSite);



            // assert
            createResult.Succeeded.Should().BeTrue();
        }

    }
}
